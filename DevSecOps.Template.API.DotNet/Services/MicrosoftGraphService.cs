using System.Net.Http.Headers;

namespace DevSecOps.Template.API.DotNet.Services;

public class MicrosoftGraphService : IIAMService
{
    private readonly Uri baseAddress = new Uri("https://graph.microsoft.com/");

    private readonly IAuthorizationService authorizationService;
    private readonly ILogger<MicrosoftGraphService> logger;
    private readonly HttpClient httpClient;

    public MicrosoftGraphService(IAuthorizationService authorizationService,
        IHttpClientFactory httpClientFactory,
        ILogger<MicrosoftGraphService> logger)
    {
        this.authorizationService = authorizationService;
        this.logger = logger;
        httpClient = httpClientFactory.CreateClient();
    }

    public async Task<string> GetBasicProfile()
    {
        var jwt = await authorizationService.GetOnBehalfOfJwt(new Uri(baseAddress, "user.read").AbsoluteUri);

        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(baseAddress, "v1.0/me").AbsoluteUri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);
        var response = await httpClient.SendAsync(request);

        return await response.Content.ReadAsStringAsync();
    }
}
