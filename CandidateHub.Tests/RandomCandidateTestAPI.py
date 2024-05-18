import requests
from faker import Faker

API_URL = 'http://localhost:5098/api/candidates'  # Change this to your API URL

# Initialize Faker
faker = Faker()

# Generate random candidate data using Faker
def generate_random_candidate():
    candidate = {
        "firstName": faker.first_name(),
        "lastName": faker.last_name(),
        "phoneNumber": faker.phone_number(),
        "email": faker.email(),
        "preferredCallTime": faker.random_element(elements=("Morning", "Afternoon", "Evening")),
        "linkedInProfileUrl": faker.url(),
        "gitHubProfileUrl": faker.url(),
        "comment": faker.text(max_nb_chars=100)
    }
    return candidate

# Add a candidate to the API
def add_candidate(candidate):
    headers = {'Content-Type': 'application/json'}
    response = requests.post(API_URL, json=candidate, headers=headers, verify=False)  # Set verify=False to ignore SSL verification
    if response.status_code == 200:
        print(f"Successfully added candidate: {candidate['email']}")
    else:
        print(f"Failed to add candidate: {candidate['email']} (Status code: {response.status_code})")

# Main function to add multiple candidates
def add_random_candidates(num_candidates):
    for _ in range(num_candidates):
        candidate = generate_random_candidate()
        add_candidate(candidate)

if __name__ == "__main__":
    num_candidates = 10  # Set the number of candidates to add
    add_random_candidates(num_candidates)
