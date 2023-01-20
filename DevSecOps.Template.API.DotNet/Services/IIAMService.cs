namespace DevSecOps.Template.API.DotNet.Services
{
    public interface IIAMService
    {
        Task<string> GetBasicProfile();
    }
}
