using NDC.Domain.Entities;

namespace NDC.Domain.Models;

public class GroupedMeteoritesResult
{
    public IEnumerable<MeteoriteGroup> Groups { get; set; }
    
    public int TotalCount { get; set; }
    
    public double? TotalMass { get; set; }
}

public class MeteoriteGroup
{
    public int Year { get; set; }
    
    public double? Mass { get; set; }
    
    public int MeteoritesCount { get; set; }
}