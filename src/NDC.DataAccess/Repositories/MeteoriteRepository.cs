using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NDC.Domain.Entities;
using NDC.Domain.Interfaces;

namespace NDC.DataAccess.Repositories;

public class MeteoriteRepository : IMeteoriteRepository
{
    private readonly MeteoritesContext _context;

    public MeteoriteRepository(MeteoritesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Meteorite>> GetAllAsync()
    {
        return await _context.Meteorites.AsNoTracking().ToListAsync();
    }

    public IQueryable<Meteorite> GetAll() => _context.Meteorites.AsQueryable();
    
    public async Task SyncMeteoritesAsync(List<Meteorite> itemsToInsert, List<Meteorite> itemsToUpdate, List<Meteorite> itemsToDelete)
    {
        var bulkConfig = new BulkConfig
        {
            PreserveInsertOrder = true,
            SetOutputIdentity = true
        };

        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        if (itemsToInsert.Any())
        {
            await _context.BulkInsertAsync(itemsToInsert, bulkConfig);
        }

        if (itemsToUpdate.Any())
        {
            await _context.BulkUpdateAsync(itemsToUpdate, bulkConfig);
        }

        if (itemsToDelete.Any())
        {
            await _context.BulkDeleteAsync(itemsToDelete, bulkConfig);
        }

        await transaction.CommitAsync();
    }
}