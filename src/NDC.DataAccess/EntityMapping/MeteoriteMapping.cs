using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDC.Domain.Entities;

namespace NDC.DataAccess.EntityMapping;

public class MeteoriteMapping : IEntityTypeConfiguration<Meteorite>
{
    public void Configure(EntityTypeBuilder<Meteorite> builder)
    {
        builder
            .ToTable("Meteorites")
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Name)
            .HasColumnType("varchar(128)")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .HasIndex(m => m.Name)
            .HasDatabaseName("IX_Meteorite_Name")
            .IsUnique(false);

        builder
            .ToTable("Meteorites")
            .HasIndex(m => m.MeteoriteId)
            .IsUnique();

        builder
            .Property(m => m.NameType)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder
            .HasOne(m => m.MeteoriteClass)
            .WithMany(mc => mc.Meteorites)
            .HasForeignKey(m => m.MeteoriteClassId);

        builder
            .Property(m => m.Mass)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder
            .Property(m => m.FallType)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();

        builder
            .Property(m => m.ObservationYear)
            .HasColumnType("date")
            .IsRequired(false);

        builder
            .HasIndex(m => m.ObservationYear)
            .HasDatabaseName("IX_Meteorite_ObservationYear");

        builder
            .Property(m => m.Reclat)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder
            .Property(m => m.Reclong)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder
            .Property(m => m.GeolocationType)
            .HasColumnType("varchar(32)")
            .IsRequired(false);

        builder
            .Property(m => m.ComputedRegionCbhk)
            .IsRequired(false);

        builder
            .Property(m => m.ComputedRegionNnqa)
            .IsRequired(false);

        builder
            .Property(m => m.Hash)
            .HasColumnType("varchar(64)")
            .HasMaxLength(64)
            .IsRequired();
    }
}