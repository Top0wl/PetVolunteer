using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.Species.Entities;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));

        builder.Property(b => b.Name)
            .IsRequired()
            .HasColumnName("name");
    }
}