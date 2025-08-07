using Crud.Models;
using Crud.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login model)
        {
            // Replace this with actual DB check later
            if (model.Username == "admin" && model.Password == "admin123")
            {
                var token = _jwtService.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid username or password");
        }
    }
}
