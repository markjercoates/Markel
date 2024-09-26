namespace Markel.Application.Entities;

public class Company : Entity
{
    public Company()
    {
    }

    public Company(int id, string name, string address1,
        string address2, string address3, string postCode,
        string country, bool active, DateTime insuranceEndDate)
    {
        Id = id;
        Name = name;
        Address1 = address1;
        Address2 = address2;
        Address3 = address3;
        PostCode = postCode;
        Country = country;
        Active = active;
        InsuranceEndDate = insuranceEndDate;
    }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string Address3 { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool Active { get; set; }
    public DateTime InsuranceEndDate { get; set; }
    public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();
}