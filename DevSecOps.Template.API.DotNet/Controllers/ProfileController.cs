using DevSecOps.Template.API.DotNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace DevSecOps.Template.API.DotNet.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> logger;
        private readonly IIAMService iamService;

        public ProfileController(IIAMService iamService, ILogger<ProfileController> logger)
        {
            this.logger = logger;
            this.iamService = iamService;
        }

        [RequiredScope(new[] { "User.Read" })]
        [HttpGet(Name = "GetProfile")]
        public async Task<IActionResult> Me()
        {

            var json = await iamService.GetBasicProfile();
            return new OkObjectResult(json);
        }
    }
}
