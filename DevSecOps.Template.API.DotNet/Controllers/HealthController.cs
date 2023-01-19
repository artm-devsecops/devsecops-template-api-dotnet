using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSecOps.Template.API.DotNet.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> logger;

        public HealthController(ILogger<HealthController> logger)
        {
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("startup", Name = "GetStartup")]
        public IActionResult GetStartup()
        {
            return new OkResult();
        }

        [AllowAnonymous]
        [HttpGet("readiness", Name = "GetReadiness")]
        public IActionResult GetReadiness()
        {
            return new OkResult();
        }

        [AllowAnonymous]
        [HttpGet("liveness", Name = "GetLiveness")]
        public IActionResult GetLiveness()
        {
            return new OkResult();
        }
         
    }
}
