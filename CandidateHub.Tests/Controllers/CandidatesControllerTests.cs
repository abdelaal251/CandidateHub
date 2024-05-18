using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHub.API.Controllers;
using CandidateHub.API.Interfaces;
using CandidateHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class CandidatesControllerTests
{
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly CandidatesController _controller;

    public CandidatesControllerTests()
    {
        _mockCandidateService = new Mock<ICandidateService>();
        _controller = new CandidatesController(_mockCandidateService.Object);
    }

    [Fact]
    public async Task GetAllCandidates_ShouldReturnOkResult()
    {
        // Arrange
        _mockCandidateService.Setup(service => service.GetAllCandidatesAsync())
            .ReturnsAsync(new List<Candidate>());

        // Act
        var result = await _controller.GetAllCandidates();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetCandidateByEmail_ShouldReturnOkResult()
    {
        // Arrange
        var email = "john.doe@example.com";
        _mockCandidateService.Setup(service => service.GetCandidateByEmailAsync(email))
            .ReturnsAsync(new Candidate { Email = email });

        // Act
        var result = await _controller.GetCandidateByEmail(email);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var candidate = Assert.IsType<Candidate>(okResult.Value);
        Assert.Equal(email, candidate.Email);
    }

    [Fact]
    public async Task CreateOrUpdateCandidate_ShouldReturnOkResult()
    {
        // Arrange
        var candidate = new Candidate
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john.doe@example.com",
            PreferredCallTime = "Morning",
            LinkedInProfileUrl = "https://www.linkedin.com/in/johndoe",
            GitHubProfileUrl = "https://github.com/johndoe",
            Comment = "Sample comment"
        };

        // Act
        var result = await _controller.CreateOrUpdateCandidate(candidate);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteCandidate_ShouldReturnNoContentResult()
    {
        // Arrange
        var email = "john.doe@example.com";

        // Act
        var result = await _controller.DeleteCandidate(email);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
