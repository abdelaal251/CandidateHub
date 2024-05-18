using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHub.API.Interfaces;
using CandidateHub.API.Models;

namespace CandidateHub.API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _candidateRepository.GetAllCandidatesAsync();
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _candidateRepository.GetCandidateByEmailAsync(email);
        }

        public async Task CreateOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _candidateRepository.GetCandidateByEmailAsync(candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;
                await _candidateRepository.UpdateCandidateAsync(existingCandidate);
            }
            else
            {
                await _candidateRepository.AddCandidateAsync(candidate);
            }
        }

        public async Task DeleteCandidateAsync(string email)
        {
            await _candidateRepository.DeleteCandidateAsync(email);
        }
    }
}
