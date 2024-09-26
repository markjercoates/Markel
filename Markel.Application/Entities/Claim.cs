namespace Markel.Application.Entities;

public class Claim : Entity
{
    public Claim()
    {
    }

    public Claim(int id, int claimTypeId, string ucr, DateTime claimDate, DateTime lossDate,
        string assuredName, decimal incurredLoss, bool closed, int companyId )
    {
        Id = id;
        ClaimTypeId = claimTypeId;
        UCR = ucr;
        ClaimDate = claimDate;
        LossDate = lossDate;
        AssuredName = assuredName;
        IncurredLoss = incurredLoss;
        Closed = closed;
        CompanyId = companyId;
    }
    public int Id { get; set; }
    public int ClaimTypeId { get; set; }
    public ClaimType ClaimType { get; set; } = null!;
    public string UCR { get; set; } = null!;
    public DateTime ClaimDate { get; set; } 
    public DateTime LossDate { get; set; }
    public string AssuredName { get; set; } = null!;
    public decimal IncurredLoss { get; set; }
    public bool Closed { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public int NumberOfDays => (DateTime.Now - ClaimDate).Days;
}