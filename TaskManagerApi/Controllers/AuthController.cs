using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.DTOs.Auth;
using TaskManagerApi.Services.Auth;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
                return BadRequest(new { errors = result.Errors });

            return StatusCode(201, new { message = "User registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result.Token == null)
                return Unauthorized(new { message = result.Error });

            return Ok(new
            {
                token = result.Token,
                fullName = result.FullName,
                email = result.Email
            });
        }
    }
}
