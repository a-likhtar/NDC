namespace NDC.Domain.Entities;

public class MeteoriteClass
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public ICollection<Meteorite> Meteorites { get; set; }
}