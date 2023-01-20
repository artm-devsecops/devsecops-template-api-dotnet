using DevSecOps.Template.API.DotNet.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DevSecOps.Template.API.DotNet.Tests.Controllers;

public class HealthControllerTests
{
    [Fact]
    public void GetStartupReturnsOk()
    {
        //arrange
        var logger = new Mock<ILogger<HealthController>>();
        var sut = new HealthController(logger.Object);


        // act
        var result = sut.GetStartup();

        // assert
        Assert.IsAssignableFrom<OkResult>(result);
    }

    [Fact]
    public void GetReadinessReturnsOk()
    {
        //arrange
        var logger = new Mock<ILogger<HealthController>>();
        var sut = new HealthController(logger.Object);


        // act
        var result = sut.GetReadiness();

        // assert
        Assert.IsAssignableFrom<OkResult>(result);
    }

    [Fact]
    public void GetLivenessReturnsOk()
    {
        //arrange
        var logger = new Mock<ILogger<HealthController>>();
        var sut = new HealthController(logger.Object);


        // act
        var result = sut.GetLiveness();

        // assert
        Assert.IsAssignableFrom<OkResult>(result);
    }
}
