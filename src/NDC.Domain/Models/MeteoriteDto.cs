using System.Text.Json.Serialization;
using NDC.Domain.Enums;

namespace NDC.Domain.Models;

public class MeteoriteDto
{
    public string Name { get; set; } = string.Empty;
    
    public int MeteoriteId { get; set; }

    public NameType NameType { get; set; }

    public string MeteoriteClass { get; set; } = string.Empty;
    
    public double? Mass { get; set; }

    public FallType FallType { get; set; }
    
    public DateOnly? ObservationYear { get; set; }
    
    public decimal? Reclat { get; set; }
    
    public decimal? Reclong { get; set; }

    public GeoLocation? GeoLocation { get; set; }
    
    public int? ComputedRegionCbhk { get; set; }
    
    public int? ComputedRegionNnqa { get; set; }
}

public class GeoLocation
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("coordinates")]
    public decimal[] Coordinates { get; set; }
}