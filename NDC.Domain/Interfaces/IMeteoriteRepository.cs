using NDC.Domain.Entities;

namespace NDC.Domain.Interfaces;

public interface IMeteoriteRepository
{
    Task<IEnumerable<Meteorite>> GetAllAsync();
    
    IQueryable<Meteorite> All { get; }
    
    Task SyncMeteoritesAsync(List<Meteorite> toInsert, List<Meteorite> toUpdate, List<Meteorite> toDelete);
}