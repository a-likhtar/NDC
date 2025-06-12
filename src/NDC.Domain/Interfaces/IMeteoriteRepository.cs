using NDC.Domain.Entities;

namespace NDC.Domain.Interfaces;

public interface IMeteoriteRepository
{
    Task<IEnumerable<Meteorite>> GetAllAsync();

    IQueryable<Meteorite> GetAll();
    
    Task SyncMeteoritesAsync(List<Meteorite> itemsToInsert, List<Meteorite> itemsToUpdate, List<Meteorite> itemsToDelete);
}