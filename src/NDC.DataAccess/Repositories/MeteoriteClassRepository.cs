using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NDC.Domain.Entities;
using NDC.Domain.Interfaces;

namespace NDC.DataAccess.Repositories;

public class MeteoriteClassRepository : IMeteoriteClassRepository
{
    public MeteoritesContext _context;

    public MeteoriteClassRepository(MeteoritesContext context)
    {
        _context = context;
    }

    // pass cancellation token
    public async Task<IEnumerable<MeteoriteClass>> GetClassesByClassNames(params string[] classNames)
    {
        return await _context.MeteoriteClasses
            .Where(mc => classNames.Contains(mc.Name))
            .ToListAsync();
    }

    public async Task<IEnumerable<MeteoriteClass>> GetAll()
    {
        return await _context.MeteoriteClasses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task BulkInsertAsync(IEnumerable<MeteoriteClass> classes)
    {
        await _context.BulkInsertAsync(classes);
    }
}