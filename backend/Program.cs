using backend.Data;
using backend.Repositories;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
       policy =>
       {
           policy.WithOrigins("http://localhost:5174").AllowAnyHeader().AllowAnyMethod().
           AllowCredentials(); // 🔥 REQUIRED FOR COOKIES;
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
app.UseCors("AllowReact");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication(); // <-- Add this BEFORE UseAuthorization
app.UseAuthorization();
app.UseExceptionHandler("/error");

 

app.MapControllers();


app.Run();