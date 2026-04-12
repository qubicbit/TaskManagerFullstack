using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Data.Dev;

namespace TaskManagerApi.Controllers
{//endast aktiv i Development

    [ApiController]
    [Route("api/dev")]
    public class DevController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public DevController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetDatabase([FromServices] IServiceProvider services)
        {
            // Blockera i produktion
            if (!_env.IsDevelopment())
                return Forbid("Not allowed outside development");

            await DevDatabaseReset.ResetAsync(services);

            return Ok("Database reset complete");
        }
    }
}
