using Castle.Core.Logging;
using DevSecOps.Template.API.DotNet.Models;
using DevSecOps.Template.API.DotNet.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevSecOps.Template.API.DotNet.Tests.Services;

public  class MicrosoftGraphServiceTests
{
    [Fact]
    public async void GetBasicProfileReturnsJson()
    {
        // arrange
        var logger = new Mock<ILogger<MicrosoftGraphService>>();
        var jwtResponse = new JwtResponse();
        var json = "{ \"givenName\": \"Bob\" }";

        var authorizationService = new Mock<IAuthorizationService>();
        authorizationService.Setup(_ => _.GetOnBehalfOfJwt(It.IsAny<string>())).Returns(Task.FromResult(jwtResponse));

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

        var sut = new MicrosoftGraphService(authorizationService.Object, httpClientFactory.Object, logger.Object);


        // act
        var result = await sut.GetBasicProfile();

        // assert
        Assert.NotEmpty(result);
        Assert.Equal(json, result);
        authorizationService.Verify(_ => _.GetOnBehalfOfJwt(It.IsAny<string>()), Times.Once);
    }
}
