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
                Active = faker.Random.Bool(),
                InsuranceEndDate = faker.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(3)),
            });
        }

        dbContext.Companies.AddRange(companies);
        dbContext.SaveChanges();

        if (!dbContext.ClaimTypes.Any())
        {
            claimTypes.Add(new ClaimType { Name = "Life" });
            claimTypes.Add(new ClaimType { Name = "Auto" });
            claimTypes.Add(new ClaimType { Name = "Health" });
            claimTypes.Add(new ClaimType { Name = "Homeowner" });
            claimTypes.Add(new ClaimType { Name = "Travel" });
            
            dbContext.ClaimTypes.AddRange(claimTypes);
            dbContext.SaveChanges();
        }
        
        var companyIds = dbContext.Companies.Select(i => i.Id).ToList();
        var claimTypesIds = dbContext.ClaimTypes.Select(i => i.Id).ToList();
       
        for (int i = 0; i < 20; i++)
        {
            claims.Add(new Claim
            {
                UCR = faker.Random.AlphaNumeric(15).ToUpper(),
                AssuredName = faker.Name.FullName(),
                ClaimTypeId = faker.PickRandom(claimTypesIds),
                CompanyId = faker.PickRandom(companyIds),
                ClaimDate = faker.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now),
                LossDate = faker.Date.Between(DateTime.Now.AddMonths(-4), DateTime.Now.AddMonths(-2)),
                Closed = faker.Random.Bool(),
                IncurredLoss = faker.Finance.Amount(100, 5000, 2)
            });
        }
        
        dbContext.Claims.AddRange(claims);
        dbContext.SaveChanges();
    }
}