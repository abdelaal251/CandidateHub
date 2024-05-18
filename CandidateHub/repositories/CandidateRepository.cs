using System.Collections.Generic;
using System.Threading.Tasks;
using CandidateHub.API.Data;
using CandidateHub.API.Interfaces;
using CandidateHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CandidateHub.API.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

        public CandidateRepository(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
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

        public async Task AddCandidateAsync(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
            _cache.Remove("GetAllCandidates"); // Invalidate the cache
        }

        public async Task UpdateCandidateAsync(Candidate candidate)
        {
            _context.Candidates.Update(candidate);
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
    }
}
