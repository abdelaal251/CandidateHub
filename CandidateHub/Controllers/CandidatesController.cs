using JobCandidateHub.API.Interfaces;
using JobCandidateHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateCandidate([FromBody] Candidate candidate)
        {
            if (candidate == null || string.IsNullOrEmpty(candidate.Email))
            {
                return BadRequest("Candidate data is invalid.");
            }

            await _candidateService.CreateOrUpdateCandidateAsync(candidate);
            return Ok();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteCandidate(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            await _candidateService.DeleteCandidateAsync(email);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCandidateByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var candidate = await _candidateService.GetCandidateByEmailAsync(email);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
    }
}
