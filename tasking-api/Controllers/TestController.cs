using Microsoft.AspNetCore.Mvc;
using tasking_api.Infrastructure.Context;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Controllers
{
    // DEBUG USE ONLY
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AppDbContext _db;

        public TestController(ILogger<LoginController> logger, IAuthService authService, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("db")]
        public async Task<IActionResult> CheckDb(CancellationToken ct)
        {
            var canConnect = await _db.Database.CanConnectAsync(ct);
            return canConnect ? Ok("Database OK") : StatusCode(500, "Database connection failed");
        }
    }
}
