using Bogus;
using Markel.Application.Entities;
using Markel.Infrastructure.Data;

namespace Markel.API.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var faker = new Faker("en_GB");
      
        var companies = new List<Company>();
        var claimTypes = new List<ClaimType>();
        var claims = new List<Claim>();

        for (int i = 0; i < 10; i++)
        {
            companies.Add(new Company
            {
                Name = faker.Company.CompanyName(),
                Address1 = faker.Address.StreetName(),
                Address2 = faker.Address.SecondaryAddress(),
                Address3 = faker.Address.City(),
                PostCode = faker.Address.ZipCode(),
                Country = "UK",
                Active = true,
                InsuranceEndDate = faker.Date.Future()
            });
        }

        dbContext.Companies.AddRange(companies);
        dbContext.SaveChanges();

        claimTypes.Add(new ClaimType{ Name = "Life" });
        claimTypes.Add(new ClaimType{ Name = "Auto" });
        claimTypes.Add(new ClaimType{ Name = "Health" });
        claimTypes.Add(new ClaimType{ Name = "Homeowner" });
        claimTypes.Add(new ClaimType{ Name = "Travel" });
        
        dbContext.ClaimTypes.AddRange(claimTypes);
        dbContext.SaveChanges();

        int claimTypeId = dbContext.ClaimTypes.First().Id;
        int companyId = dbContext.Companies.First().Id;
        claims.Add(new Claim
        {
            UCR = "AA123456789", 
            AssuredName = faker.Person.FullName,
            ClaimTypeId = claimTypeId,
            CompanyId = companyId,
            ClaimDate = DateTime.Now.AddDays(-7),
            LossDate = DateTime.Now.AddDays(-10),
            Closed = false,
            IncurredLoss = 1000.00m,
        });
        
        dbContext.Claims.AddRange(claims);
        dbContext.SaveChanges();
    }
}