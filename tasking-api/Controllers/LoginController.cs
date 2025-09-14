using Microsoft.AspNetCore.Mvc;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        private readonly IAuthService _authService;
        private readonly AppDbContext _db;

        public LoginController(ILogger<LoginController> logger, IAuthService authService, AppDbContext db)
        {
            _logger = logger;
            _authService = authService;
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request, ct);

            return result.Success
                ? Ok()
                : BadRequest(result.Error);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(request, ct);

            return result.Success
                ? Ok()
                : BadRequest(result.Error);
        }

        [HttpPost("googleAuthLogin")]
        public async Task<IActionResult> GoogleAuthLogin()
        {
            return Ok("Req sent");
        }

        [HttpPost("googleAuthCallback")]
        public async Task<IActionResult> GoogleAuthCallback()
        {
            return Ok("Req sent");
        }
    }
}
