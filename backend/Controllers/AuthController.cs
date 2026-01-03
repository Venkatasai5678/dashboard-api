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
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null) return Unauthorized();

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }
}
