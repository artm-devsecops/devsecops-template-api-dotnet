using Xunit;
using DevSecOps.Template.API.DotNet.Controllers;
using DevSecOps.Template.API.DotNet.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;

namespace DevSecOps.Template.API.DotNet.Tests.Controllers;

public class ProfileControllerTests
{
    [Fact]
    public async void GetBasicProfileReturnsPayload()
    {
        //arrange
        var iamService = new Moq.Mock<IIAMService>();
        var logger = new Moq.Mock<ILogger<ProfileController>>();
        var sut = new ProfileController(iamService.Object, logger.Object);
        var json = "{ \"givenName\": \"Bob\" }";

        iamService.Setup(x => x.GetBasicProfile()).Returns(Task.FromResult(json));

        // act
        var result = await sut.Me();

        // assert
        var action = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.Equal(json, action.Value);
        iamService.Verify(x => x.GetBasicProfile(), Times.Once);
    }
}
