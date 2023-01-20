using DevSecOps.Template.API.DotNet.Options;
using DevSecOps.Template.API.DotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace DevSecOps.Template.API.DotNet.Tests.Services;

public class AzureADAuthorizationServiceTests
{
    [Fact]
    public async void GetOnBehalfOfJwtReturnsResponse()
    {
        // arrange
        var logger = new Mock<ILogger<AzureADAuthorizationService>>();
        var options = new Mock<IOptions<AzureADRegistrationOptions>>();
        var azureADRegistrationOptions = new AzureADRegistrationOptions
        {
            TenantId = nameof(AzureADRegistrationOptions.TenantId),
            ClientId = nameof(AzureADRegistrationOptions.ClientId),
            ClientSecret = nameof(AzureADRegistrationOptions.ClientSecret)
        };
        options.Setup(_ => _.Value).Returns(azureADRegistrationOptions);

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var context = new DefaultHttpContext();
        context.Request.Headers["Authorization"] = "Bearer this_is_an_access_token";
        httpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

        var scope = "test_scope";
        var json = "{ \"access_token\": \"test\" }";

        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpMessageHandler = new Mock<HttpMessageHandler>();

        var response = new HttpResponseMessage();
        response.Content = new StringContent(json);

        httpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response)
            .Verifiable();

        var httpClient = new HttpClient(httpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://test.dev/")
        };

        httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var sut = new AzureADAuthorizationService(options.Object, httpClientFactory.Object, httpContextAccessor.Object, logger.Object);

        // act
        var jwtResponse = await sut.GetOnBehalfOfJwt(scope);

        // assert
        Assert.NotNull(jwtResponse);
        Assert.Equal("test", jwtResponse.AccessToken);
    }
}
