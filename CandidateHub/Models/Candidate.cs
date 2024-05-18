namespace CandidateHub.API.Models
{
    public class Candidate
    {
        public int Id { get; set; } // Primary key for database
        public string FirstName { get; set; } // First name of the candidate
        public string LastName { get; set; } // Last name of the candidate
        public string PhoneNumber { get; set; } // Phone number of the candidate
        public string Email { get; set; } // Email of the candidate (unique identifier)
        public string PreferredCallTime { get; set; } // Time interval when it’s better to call
        public string LinkedInProfileUrl { get; set; } // LinkedIn profile URL
        public string GitHubProfileUrl { get; set; } // GitHub profile URL
        public string Comment { get; set; } // Free text comment
    }

}
