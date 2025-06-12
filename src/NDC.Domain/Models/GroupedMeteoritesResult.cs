using NDC.Domain.Entities;

namespace NDC.Domain.Models;

public class GroupedMeteoritesResult
{
    public int Year { get; set; }
    
    public List<Meteorite> Items { get; set; }
}