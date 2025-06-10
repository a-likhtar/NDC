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
        return await All.AsNoTracking().ToListAsync();
    }

    public IQueryable<Meteorite> All => _context.Set<Meteorite>().AsNoTracking();
    public async Task SyncMeteoritesAsync(List<Meteorite> toInsert, List<Meteorite> toUpdate, List<Meteorite> toDelete)
    {
        var bulkConfig = new BulkConfig
        {
            PreserveInsertOrder = true,
            SetOutputIdentity = true
        };

        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        if (toInsert.Any())
        {
            await _context.BulkInsertAsync(toInsert, bulkConfig);
        }

        if (toUpdate.Any())
        {
            await _context.BulkUpdateAsync(toUpdate, bulkConfig);
        }

        if (toDelete.Any())
        {
            await _context.BulkDeleteAsync(toDelete, bulkConfig);
        }

        await transaction.CommitAsync();
    }
}