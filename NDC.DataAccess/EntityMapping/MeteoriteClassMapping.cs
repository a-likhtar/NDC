using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDC.DataAccess.Entities;

namespace NDC.DataAccess.EntityMapping;

public class MeteoriteClassMapping : IEntityTypeConfiguration<MeteoriteClass>
{
    public void Configure(EntityTypeBuilder<MeteoriteClass> builder)
    {
        builder
            .ToTable("MeteoriteClasses")
            .HasKey(mc => mc.Id);

        builder
            .Property(mc => mc.Name)
            .HasColumnType("varchar(128)")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .HasIndex(mc => mc.Name)
            .IsUnique();
    }
}