namespace Markel.Application.Entities;

public class ClaimType : Entity
{
    public ClaimType()
    {
    }

    public ClaimType(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Claim> Claims { get; set; } = new HashSet<Claim>();
}