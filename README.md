# Candidate Hub API

## Overview

This project is a web application API for storing and managing job candidate information. The application is built using the .NET stack and follows the Repository Design Pattern for data access abstraction. It supports basic CRUD operations for candidate data, with caching implemented for improved performance.

## Features

- Add or update candidate information.
- Retrieve all candidates.
- Retrieve a specific candidate by email.
- Delete a candidate by email.
- Caching to improve performance.

## Technologies Used

- .NET Core
- Entity Framework Core
- SQL Server
- MemoryCache for caching
- xUnit and Moq for unit testing
- Faker for generating random data in Python

## Setup Instructions

### .NET Setup

1. Clone the Repository
   ```sh
   `git clone https://github.com/abdelaal251/CandidateHub.git`

2. Open the Solution
	Open the solution in Visual Studio.

3. Update Connection String
	Update the connection string in appsettings.json to point to your SQL Server instance.

4. Update-Database 

5. Run the Application

#	API Endpoints

## Base URL

Assuming the API is deployed locally, the base URL could be:

http://localhost:5098/api/candidates

## Endpoints

POST /api/candidates
- **Descripton:** Adds a new candidate or updates an existing candidate's information based on the email provided.
- **Parameters:** A candicate object contains first name, last name, email, phone number, etc.
- **Response:** `200 OK`: If the candidate is successfully added or updated.

GET /api/candidates
- **Description:** Retrieves a list of all candidates.
- **Response:** `200 OK`: Returns an array of candidate objects.

GET /api/candidates/{email}
- **Description:** Retrieves one candidate object.
- **Response:** `200 OK`: Returns a one candidate object.

DELETE /api/candidates/{email}
- **Description:** Deletes one candidate object.
- **Response:** `200 OK`: Deletes a one candidate object.

Error Handling
**invalid input** `400 bad request`
**internal issues** `500 internal server error`


# Unit Tests for Candidate Hub API

## Overview

This Part provides an overview of the unit tests for the Candidate Hub API. Unit tests are essential for ensuring the correctness and reliability of the application by testing individual components in isolation.

## Setup

- `GetCandidateByEmailAsync_ShouldReturnCandidate`: returns the correct candidate by email.
- `AddOrUpdateCandidateAsync_ShouldAddCandidate`: adds a new candidate when the email does not exist in the database.
- `AddOrUpdateCandidateAsync_ShouldUpdateCandidate`: updates an existing candidate when the email already exists in the database.
- `DeleteCandidateByEmailAsync_ShouldRemoveCandidate`: deletes a candidate by email.

## Run the test
- Navigate to the Test Project Directory
- Run the Tests `dotnet test`

# Potential Improvements

- **Distributed Caching:**
   - **Rationale:** Using distributed caching solutions like Redis can improve performance and scalability, especially in a multi-server environment.
   - **Implementation:** Replace the in-memory cache with Redis or another distributed caching solution.

- **Error Handling and Validation:**
   - **Rationale:** Enhance the robustness of the API by adding comprehensive error handling and validation.
   - **Implementation:** Use FluentValidation for model validation and implement global exception handling using middleware.

- **Logging:**
   - **Rationale:** Implementing logging can help in monitoring and troubleshooting the application.
   - **Implementation:** Use a logging framework like Serilog or NLog to log important events and errors.

- **Pagination for Get All Candidates:**
   - **Rationale:** For a large number of candidates, returning all records in one response is inefficient.
   - **Implementation:** Implement pagination for the `GET /api/candidates` endpoint to return a subset of candidates with each request.

- **Authentication and Authorization:**
   - **Rationale:** Secure the API by ensuring that only authorized users can access and modify candidate data.
   - **Implementation:** Use ASP.NET Core Identity or JWT for authentication and authorization.

- **Database Migrations:**
   - **Rationale:** Ensure smooth database updates and version control.
   - **Implementation:** Use Entity Framework Core Migrations to handle database schema changes.

# List of Assumptions

- **Database Choice:**
   - SQL Server is used for this implementation, but the design is kept flexible for future migration to other types of databases.

- **Caching Strategy:**
   - In-memory caching is implemented assuming the application runs on a single server. For a distributed environment, a distributed caching solution like Redis would be more appropriate.

- **Email as Unique Identifier:**
   - It is assumed that the email provided for each candidate is unique and is used as the primary key for identification.

- **API Endpoint:**
   - The API is designed with a single endpoint for create and update operations based on the email provided in the request.

# Deliverables list

## Source Code

- All source code is hosted on GitHub in a version-controlled repository.

## Potential Improvements

- added in the README.md

## List of assumptions

- add in the README.md

## Total Time Spent 

- as per time tracking for each task and subtask during the development process
-- Development (5 hrs).
-- Documentations (1 hrs).
-- Testing (1 hr).
total time spent to finish the assesment is 7 hours.


