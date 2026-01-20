using Microsoft.AspNetCore.Mvc;
using backend.MODEL;
using backend.Services;
using backend.Data;
using System.Linq;
using ModelEntity.MODEL;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            // 1️⃣ Validate username & password (DB check)
            var user = _context.Users
                .FirstOrDefault(u => u.Username == login.Username
                                  && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // 2️⃣ Create JWT (same as before)
            var token = _authService.GenerateJwtToken(user);

            // 3️⃣ STORE JWT IN HTTP-ONLY COOKIE ✅
            Response.Cookies.Append(
                "access_token",              // cookie name
                token,                       // JWT value
                new CookieOptions
                {
                    HttpOnly = true,          // 🔐 JS cannot read
                    Secure = false,           // true in production (HTTPS)
                    SameSite = SameSiteMode.Strict, // 🛡 CSRF protection
                    Expires = DateTime.UtcNow.AddMinutes(60)
                }
            );

            // 4️⃣ Return simple response (NO TOKEN)
            return Ok(new { message = "Login successful" });
        }

    }
}
