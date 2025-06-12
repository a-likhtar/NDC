using Microsoft.EntityFrameworkCore;
using NDC.DataAccess.EntityMapping;
using NDC.Domain.Entities;

namespace NDC.DataAccess;

public class MeteoritesContext : DbContext
{
    public MeteoritesContext(DbContextOptions<MeteoritesContext> options) : base(options)
    {
    }

    public DbSet<Meteorite> Meteorites => Set<Meteorite>();

    public DbSet<MeteoriteClass> MeteoriteClasses => Set<MeteoriteClass>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MeteoriteMapping());
        modelBuilder.ApplyConfiguration(new MeteoriteClassMapping());
    }
}