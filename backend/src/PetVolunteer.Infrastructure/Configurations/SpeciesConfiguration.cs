using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.Species;
using PetVolunteer.Domain.Species.Entities;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(s => s.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.ComplexProperty(s => s.TypeAnimal, sb =>
        {
            sb.Property(ta => ta.Name)
                .IsRequired()
                .HasColumnName("name");
        });

        builder
            .HasMany(s => s.Breeds)
            .WithOne();
    }
}