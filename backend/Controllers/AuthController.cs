using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Data;
using backend.MODEL;
using ModelEntity.MODEL;
using System;
using System.Linq;

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

        // ===================== LOGIN =====================
        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            // 1️⃣ Validate username & password
            // WHY: Only authenticated users should receive tokens
            //
            var user = _context.Users
                .FirstOrDefault(u =>
                    u.Username == login.Username &&
                    u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // 2️⃣ Generate ACCESS TOKEN (JWT – 15 mins)
            // WHY: Used to authorize API requests
            var accessToken = _authService.GenerateJwtToken(user);

            // 3️⃣ Generate REFRESH TOKEN (7 days)
            // WHY: Used to silently renew access token
            var refreshToken = _authService.GenerateRefreshToken();

            // 4️⃣ Save refresh token in DB
            // WHY: Refresh tokens can be revoked (JWT cannot)
            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                Username = user.Username,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });
            _context.SaveChanges();

            // 5️⃣ Store ACCESS TOKEN in HttpOnly cookie
            Response.Cookies.Append("access_token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true in production (HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            // 6️⃣ Store REFRESH TOKEN in HttpOnly cookie
            Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            // 7️⃣ Return response (NO TOKENS SENT TO CLIENT)
            return Ok(new { message = "Login successful" });
        }

        // ===================== REFRESH =====================
        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            // 1️⃣ Read refresh token from cookie
            if (!Request.Cookies.TryGetValue("refresh_token", out var refreshToken))
                return Unauthorized();

            // 2️⃣ Validate refresh token from DB
            var storedToken = _context.RefreshTokens.FirstOrDefault(rt =>
                rt.Token == refreshToken &&
                !rt.IsRevoked &&
                rt.ExpiresAt > DateTime.UtcNow
            );
            //hgjhgbngffghfgh
            if (storedToken == null)
                return Unauthorized();

            // 3️⃣ Get user
            var user = _context.Users
                .FirstOrDefault(u => u.Username == storedToken.Username);

            if (user == null)
                return Unauthorized();

            // 4️⃣ Issue NEW access token (JWT – 15 mins)
            var newAccessToken = _authService.GenerateJwtToken(user);

            // 5️⃣ Update access token cookie
            Response.Cookies.Append("access_token", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            if (storedToken == null)
                return Unauthorized();

            // 3️⃣ Get user
            var user = _context.Users
                .FirstOrDefault(u => u.Username == storedToken.Username);

            if (user == null)
                return Unauthorized();

            // 4️⃣ Issue NEW access token (JWT – 15 mins)
            var newAccessToken = _authService.GenerateJwtToken(user);

            // 5️⃣ Update access token cookie
            Response.Cookies.Append("access_token", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(15)
            });

            return Ok();
        }
    }
}
