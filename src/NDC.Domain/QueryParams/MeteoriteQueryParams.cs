using System.ComponentModel.DataAnnotations;

namespace NDC.Domain.QueryParams;

public class MeteoriteQueryParams : IValidatableObject
{
    [StringLength(128)]
    public string? NameContains { get; set; }
    
    [Range(1, int.MaxValue)]
    public int? MeteoriteClassId { get; set; }
    
    [Range(0, 2025)]
    public int? YearFrom { get; set; }
    
    [Range(0, 2025)]
    public int? YearTo { get; set; }
    
    [AllowedValues("Year", "Count", "TotalMass")]
    public string? SortBy { get; set; }

    public bool SortDescending { get; set; } = false;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (YearFrom.HasValue && YearTo.HasValue && YearFrom > YearTo)
        {
            yield return new ValidationResult("YearFrom cannot be more than YearTo.",
                [nameof(YearFrom), nameof(YearTo)]);
        }
    }
}