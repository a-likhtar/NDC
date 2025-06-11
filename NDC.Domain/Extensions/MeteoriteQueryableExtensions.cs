using NDC.Domain.Entities;
using NDC.Domain.QueryParams;

namespace NDC.Domain.Extensions;

public static class MeteoriteQueryableExtensions
{
    public static IQueryable<Meteorite> ApplyFiltering(this IQueryable<Meteorite> query,
        MeteoriteQueryParams queryParams)
    {
        query = query.Where(m => m.ObservationYear != null);
        
        if (!string.IsNullOrEmpty(queryParams.NameContains))
        {
            query = query.Where(m => m.Name.Contains(queryParams.NameContains));
        }

        if (queryParams.MeteoriteClassId.HasValue)
        {
            query = query.Where(m => m.MeteoriteClassId == queryParams.MeteoriteClassId.Value);
        }

        if (queryParams.YearFrom.HasValue)
        {
            query = query.Where(m => m.ObservationYear != null && m.ObservationYear.Value.Year >= queryParams.YearFrom.Value);
        }

        if (queryParams.YearTo.HasValue)
        {
            query = query.Where(m => m.ObservationYear != null && m.ObservationYear.Value.Year <= queryParams.YearTo.Value);
        }

        return query;
    }

    public static IEnumerable<IGrouping<int, Meteorite>> ApplySortingToGroups(
        this IEnumerable<IGrouping<int, Meteorite>> groupedData,
        MeteoriteQueryParams queryParams)
    {
        return queryParams.SortBy switch
        {
            "Year" => queryParams.SortDescending
                ? groupedData.OrderByDescending(g => g.Key)
                : groupedData.OrderBy(g => g.Key),
            
            "Count" => queryParams.SortDescending
                ? groupedData.OrderByDescending(g => g.Count())
                : groupedData.OrderBy(g => g.Count()),
            
            "TotalMass" => queryParams.SortDescending
                ? groupedData.OrderByDescending(g => g.Sum(m => m.Mass))
                : groupedData.OrderBy(g => g.Sum(m => m.Mass)),
            
            _ => groupedData.OrderBy(g => g.Key)
        };
    }
}