using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.Models;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using Constants = PetVolunteer.Domain.Shared.Constants;

namespace PetVolunteer.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(p => p.Description)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

        
        builder.OwnsOne(p => p.Photos, pb =>
        {
            pb.ToJson();
            pb.OwnsMany(ppl => ppl.Photos, b =>
            {
                b.Property(p => p.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });
        });
        
        builder.OwnsOne(p => p.Requisites, r =>
        {
            r.ToJson();
            r.OwnsMany(pr => pr.Requisites, b =>
            {
                b.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                b.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });
        
        builder.OwnsOne(p => p.Requisites, r =>
        {
            r.ToJson();
            r.OwnsMany(pr => pr.Requisites, b =>
            {
                b.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                b.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });
    }
}