using NDC.DataAccess.Enums;

namespace NDC.DataAccess.Entities;

public class Meteorite
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public int MeteoriteId { get; set; }
    
    public NameType NameType { get; set; }

    public MeteoriteClass MeteoriteClass { get; set; }
    
    public int MeteoriteClassId { get; set; }
    
    public double? Mass { get; set; }
    
    public FallType FallType { get; set; }
    
    public DateOnly? ObservationYear { get; set; }
    
    public decimal? Reclat { get; set; }
    
    public decimal? Reclong { get; set; }

    public string? GeolocationType { get; set; }
    
    public int? ComputedRegionCbhk { get; set; }
    
    public int? ComputedRegionNnqa { get; set; }
}