using DevSecOps.Template.API.DotNet.Extensions;
using DevSecOps.Template.API.DotNet.Models;
using DevSecOps.Template.API.DotNet.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace DevSecOps.Template.API.DotNet.Services;

public class AzureADAuthorizationService : IAuthorizationService
{
    private readonly Uri baseAddress = new Uri("https://login.microsoftonline.com/");

    private readonly IOptions<AzureADRegistrationOptions> options;
    private readonly ILogger<AzureADAuthorizationService> logger;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly HttpClient httpClient;

    public AzureADAuthorizationService(IOptions<AzureADRegistrationOptions> options,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AzureADAuthorizationService> logger)
    {
        this.options = options;
        this.logger = logger;
        this.httpContextAccessor = httpContextAccessor;
        httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(baseAddress, $"{options.Value.TenantId}/");
    }

    public async Task<JwtResponse> GetOnBehalfOfJwt(string scope)
    {
        var accessToken = httpContextAccessor.HttpContext.Request.Headers.AccessToken();

        var httpContent = new FormUrlEncodedContent(new Dictionary<string, string>{
                { "grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer" },
                { "client_id", options.Value.ClientId },
                { "client_secret", options.Value.ClientSecret },
                { "assertion", accessToken },
                { "scope", scope },
                { "requested_token_use", "on_behalf_of" }
         });

        var response = await httpClient.PostAsync("oauth2/v2.0/token", httpContent);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JwtResponse>(json);
    }
}
