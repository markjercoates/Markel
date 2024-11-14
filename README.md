### Programming Test to build an .NET Core API and back end.

## Requirements

- API to allow access to Claims and Company data.
- The data can be generated in code rather than coming from SQL server.
- Output should be in json format.
- We need an endpoint that will give me a single company. We need a property to be returned that will tell us if the company has an active insurance policy
- We need an endpoint that will give me a list of claims for one company
- We need an endpoint that will give me the details of one claim. We need a property to be returned that tells us how old the claim is in days.
- We need an endpoint that will allow us to update a claim
- We need at least one unit test to be created

# Database Structure
CREATE TABLE Claims
(
	UCR VARCHAR(20),
	CompanyId INT,
	ClaimDate DATETIME,
	LossDate DATETIME,
	[Assured Name] VARCHAR(100),
	[Incurred Loss] DECIMAL(15,2),
	Closed BIT
)

CREATE TABLE ClaimType
(
	Id INT,
	Name VARCHAR(20)
)

CREATE TABLE Company
(
	Id INT,
	Name VARCHAR(200),
	Address1 VARCHAR(100),
	Address2 VARCHAR(100),
	Address3 VARCHAR(100),
	Postcode VARCHAR(20),
	Country VARCHAR(50),
	Active BIT,
	InsuranceEndDate DATETIME
)

Description

### A solution built with Visual Studio 2022, .NET 8, ASP.NET Core API, MediatR, EntityFrameworkCore, SQlite, xUnit, FluentAssertions, FluentValidation.

Demonstrates using a Clean Architecture approach (with DDD) for CRUD endpoints in the API with a CQRS pattern implemented using MediatR.

Mark Coates
