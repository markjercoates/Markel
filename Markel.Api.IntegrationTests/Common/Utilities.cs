using Markel.Application.Entities;
using Markel.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markel.Api.IntegrationTests.Common;
public class Utilities
{
    public static void SeedTestData(ApplicationDbContext context)
    {
        context.ClaimTypes.AddRange(new[]
            {
                new ClaimType
                {
                 Name = "Claim Type 1",
                },
                new ClaimType
                {
                Name = "Claim Type 2",
                }
            });

        context.SaveChanges();

        context.Companies.AddRange(new[]
        {
                new Company
                {
                Name = "Company 1",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(7),
                },
                new Company
                {
                Name = "Company 2",
                Address1 = "Address 1",
                Address2 = "Address 2",
                Address3 = "Address 3",
                PostCode = "Post Code",
                Country = "Country",
                Active = true,
                InsuranceEndDate = DateTime.Now.AddDays(-7),
                }
             });

        context.SaveChanges();

        context.Claims.AddRange(new[]
        {
            new Claim
            {
                CompanyId = 1,
                ClaimTypeId = 1,
                ClaimDate = DateTime.Now.AddDays(-1),
                LossDate = DateTime.Now.AddDays(7),
                AssuredName = "Assured Name 1",
                UCR = "UCR1",
                Closed = false,
                IncurredLoss = 1000,
            },
            new Claim
            {
                CompanyId = 1,
                ClaimTypeId = 2,
                ClaimDate = DateTime.Now.AddDays(-2),
                LossDate = DateTime.Now.AddDays(-7),
                AssuredName = "Assured Name 2",
                UCR = "UCR2",
                Closed = false,
                IncurredLoss = 2000,
            },
            new Claim
            {
                CompanyId = 2,
                ClaimTypeId = 2,
                ClaimDate = DateTime.Now.AddDays(-3),
                LossDate = DateTime.Now.AddDays(-7),
                AssuredName = "Assured Name 3",
                UCR = "UCR3",
                Closed = false,
                IncurredLoss = 3000,
            }
        });

        context.SaveChanges();
    }
}

