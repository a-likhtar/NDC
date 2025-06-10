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
}