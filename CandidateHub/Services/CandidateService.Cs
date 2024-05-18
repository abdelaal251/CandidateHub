using CandidateHub.API.Data;
using CandidateHub.API.Interfaces;
using CandidateHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;

        public CandidateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrUpdateCandidateAsync(Candidate candidate)
        {
            var existingCandidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;
                _context.Candidates.Update(existingCandidate);
            }
            else
            {
                await _context.Candidates.AddAsync(candidate);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCandidateAsync(string email)
        {
            var candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
        }
    }
}
