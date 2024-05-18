using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandidateHub.API.Data;
using CandidateHub.API.Models;
using CandidateHub.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class CandidateServiceTests
{
    private async Task<ApplicationDbContext> GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "CandidateHubTest")
            .Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();
        if (!await databaseContext.Candidates.AnyAsync())
        {
            databaseContext.Candidates.Add(new Candidate
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                PreferredCallTime = "Morning",
                LinkedInProfileUrl = "https://www.linkedin.com/in/johndoe",
                GitHubProfileUrl = "https://github.com/johndoe",
                Comment = "Sample comment"
            });
            await databaseContext.SaveChangesAsync();
        }
        return databaseContext;
    }

    [Fact]
    public async Task GetAllCandidatesAsync_ShouldReturnAllCandidates()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var candidateService = new CandidateService(dbContext);

        // Act
        var result = await candidateService.GetAllCandidatesAsync();

        // Assert
        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async Task GetCandidateByEmailAsync_ShouldReturnCandidate()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var candidateService = new CandidateService(dbContext);
        var email = "john.doe@example.com";

        // Act
        var result = await candidateService.GetCandidateByEmailAsync(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
    }

    [Fact]
    public async Task CreateOrUpdateCandidateAsync_ShouldAddCandidate()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var candidateService = new CandidateService(dbContext);
        var newCandidate = new Candidate
        {
            FirstName = "Jane",
            LastName = "Doe",
            PhoneNumber = "0987654321",
            Email = "jane.doe@example.com",
            PreferredCallTime = "Evening",
            LinkedInProfileUrl = "https://www.linkedin.com/in/janedoe",
            GitHubProfileUrl = "https://github.com/janedoe",
            Comment = "New candidate"
        };

        // Act
        await candidateService.CreateOrUpdateCandidateAsync(newCandidate);
        var result = await candidateService.GetCandidateByEmailAsync(newCandidate.Email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newCandidate.Email, result.Email);
    }

    [Fact]
    public async Task DeleteCandidateAsync_ShouldRemoveCandidate()
    {
        // Arrange
        var dbContext = await GetDatabaseContext();
        var candidateService = new CandidateService(dbContext);
        var email = "john.doe@example.com";

        // Act
        await candidateService.DeleteCandidateAsync(email);
        var result = await candidateService.GetCandidateByEmailAsync(email);

        // Assert
        Assert.Null(result);
    }
}
