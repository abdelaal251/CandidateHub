using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHub.API.Data;
using CandidateHub.API.Interfaces;
using CandidateHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateHub.API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public CandidateService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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
            _cache.Remove("GetAllCandidates"); // Invalidate the cache
            _cache.Remove($"Candidate_{candidate.Email}"); // Invalidate the cache
        }

        public async Task DeleteCandidateAsync(string email)
        {
            var candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
                _cache.Remove("GetAllCandidates"); // Invalidate the cache
                _cache.Remove($"Candidate_{email}"); // Invalidate the cache
            }
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            if (!_cache.TryGetValue("GetAllCandidates", out IEnumerable<Candidate> candidates))
            {
                candidates = await _context.Candidates.ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(_cacheExpiration);
                _cache.Set("GetAllCandidates", candidates, cacheEntryOptions);
            }
            return candidates;
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            if (!_cache.TryGetValue($"Candidate_{email}", out Candidate candidate))
            {
                candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
                if (candidate != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(_cacheExpiration);
                    _cache.Set($"Candidate_{email}", candidate, cacheEntryOptions);
                }
            }
            return candidate;
        }
    }
}
