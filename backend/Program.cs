using backend.Data;
using backend.MiddleWares;
using backend.Repositories;
using backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)

    // ❌ Errors → FILE
    .WriteTo.File(
        "Logs/errors-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Error
    )

    // ✅ Success + Errors → DB
    .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "ApplicationLogs",
            AutoCreateSqlTable = true
        },
        restrictedToMinimumLevel: LogEventLevel.Information
    )

    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
Log.Information("🚀 Serilog startup test log");

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReact",
//       policy =>
//       {
//           policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().
//           AllowCredentials(); // 🔥 REQUIRED FOR COOKIES;
//       });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerEditRepository, CustomerEditRepository>();
builder.Services.AddScoped<ICustomerEditService, CustomerEditService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerServices>();
builder.Services.AddScoped<iDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<iDashboardservice, Dashboardservice>();
 

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        RoleClaimType = ClaimTypes.Role // IMPORTANT FIX 🚀
    };
});

var app = builder.Build();

// 🔥 1. CORS MUST BE FIRST
app.UseCors("AllowReact");

// 🔥 2. Global exception handling (wraps everything)
app.UseMiddleware<GlobalExceptionMiddleware>();

// 🔥 3. HTTPS redirect
app.UseHttpsRedirection();

// 🔥 4. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 🔥 5. Controllers
app.MapControllers();

app.Run();
