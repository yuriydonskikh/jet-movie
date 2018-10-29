# JetMovie project

### Prerequisites
  
  a. Visual Studio 2017 any edition,  version 15.7 or above
  
  b. .Net Core SDK version 2.1
  
  C. NodeJS version 10 or above
  
  d. NPM version 6 or above
  
  e. Angular CLI version 7.0.3 or above
  
  f. TypeScript version 3.1.3
  
  
### Build & Run
  
  a. Open command line with Administrator rights and navigate into JetMovie\JetMovie folder
  
  b. Execute npm install
  
  c. Open solution in Visual Studio
  
  d. Install all requested updates (happened if Visual Studio is not up to date, or missed important components)
  
  e. Choose **Build Solution** from **Build** menu
  
  f. Click **Start Debugging** from **Debug** menu
  
  *Above steps have to download all npm and nuget packages rebuild project and start default browser with* **Welcome page**
  
### Used non default components and fremeworks
  
  a. AutoMapper - *mapping tool for converting classes between Entities and DTOs*
  
  b. NLog - *logging errors*
  
  c. FluentValidation - *validate data before executing controller*
  
  d. Sqlite - *Simple database for data persistent*
  
  e. Swashbuckle Swagger - *WEB Api documentation and testing*
  
  f. xUnit - *testing framework*
  
  g. AutoFixture - *tool for creating prepopulated objects for testing*
  
  h. FluentAssertions - *tool for conditional testing*
  
  i. Moq - *Mocking tool for mocking object behaveour during testing*
  
  *All above pachages will be automatically downloaded during first build*
  
### Unit and integration Tests
  
  Solution contains **21** tests which are covered all sensitive areas of projects
  
  a. AccountControllerTests (1) - *contains integration test for account controller*
  
  b. AuthControllerTests (1) - *contains integration test for auth controller*
  
  c. CartControllerTests (4) - *contains integration tests for cart controller*
  
  d. CinemaworldControllerTests (2) - *contains integration tests for cinemaworld controller*
  
  e. FilmworldControllerTests (2) - *contains integration tests for filmworld controller*
  
  f. MovieInfoControllerTests (2) - *contains integration tests for movie info controller*

  f. ExtensionsTests (1) - *contains unit test for data monipulation extension*
  
  g. MappingTests (5) - *contains unit tests for all mappers*
  
  h. ValidatorsTests (3) - *contains unit tests for all validators*
  
### Run Tests

  a. Run from Visual Studio - Choose **Test->Run->All Tests**
  
  b. Run from command prompt - from JetMovie.Tests folder execute command **dotnet test**

### Migrate to another database

  Solution contains migration script for creating database from POCO entities inside the solution also it uses for emulate database during tests.
  