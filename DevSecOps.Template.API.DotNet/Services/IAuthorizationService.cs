using DevSecOps.Template.API.DotNet.Models;

namespace DevSecOps.Template.API.DotNet.Services
{
    public interface IAuthorizationService
    {
        Task<JwtResponse> GetOnBehalfOfJwt(string scope);
    }
}
