using CandidateHub.API.Models;

namespace CandidateHub.API.Interfaces
{
    public interface ICandidateService
    {
        Task CreateOrUpdateCandidateAsync(Candidate candidate);
        Task DeleteCandidateAsync(string email);
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> GetCandidateByEmailAsync(string email);
    }
}
