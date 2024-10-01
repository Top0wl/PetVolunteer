using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using PetVolunteer.Domain.Volunteer.Entities;
using Constants = PetVolunteer.Domain.Shared.Constants;

namespace PetVolunteer.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        
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

        builder.ComplexProperty(p => p.Address, pb =>
        {
            pb.Property(a => a.City)
                .IsRequired()
                .HasColumnName("city");
            pb.Property(a => a.Street)
                .IsRequired()
                .HasColumnName("street");
            pb.Property(a => a.NumberHouse)
                .HasColumnName("number_house");
        });
            
        builder.ComplexProperty(p => p.OwnerPhoneNumber, pb =>
        {
            pb.Property(phone => phone.Value)
                .HasColumnName("owner_phone_number");
        });
        
        builder.ComplexProperty(p => p.HealthInformation, pb =>
        {
            pb.Property(healthInformation => healthInformation.Weight)
                .IsRequired()
                .HasColumnName("weight");
            pb.Property(healthInformation => healthInformation.Height)
                .IsRequired()
                .HasColumnName("height");
            pb.Property(healthInformation => healthInformation.IsCastrated)
                .IsRequired()
                .HasColumnName("is_castrated");
            pb.Property(healthInformation => healthInformation.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");
            pb.Property(healthInformation => healthInformation.AdditionalHealthInformation)
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("additional_health_information");
        });

        builder.ComplexProperty(p => p.TypeDetails, pb =>
        {
            pb.Property(td => td.SpeciesId)
                .HasConversion(id => id.Value, value => SpeciesId.Create(value))
                .IsRequired()
                .HasColumnName("species_id");
            
            pb.Property(td => td.BreedId)
                .HasConversion(id => id.Value, value => BreedId.Create(value))
                .IsRequired()
                .HasColumnName("breed_id");
        });
        
        builder.OwnsOne(p => p.Photos, pb =>
        {
            pb.ToJson("photos");
            pb.OwnsMany(ppl => ppl.Photos, b =>
            {
                b.Property(p => p.Path)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                b.Property(p => p.IsMain);
            });
        });
        
        builder.OwnsOne(p => p.Requisites, r =>
        {
            r.ToJson("requisites");
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