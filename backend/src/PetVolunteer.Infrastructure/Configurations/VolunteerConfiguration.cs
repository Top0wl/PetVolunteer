using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.Models;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using Constants = PetVolunteer.Domain.Shared.Constants;

namespace PetVolunteer.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));
        
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
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
        
        builder.OwnsOne(p => p.SocialMediaList, sm =>
        {
            sm.ToJson();
            sm.OwnsMany(vsm => vsm.SocialMedias, b =>
            {
                b.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                b.Property(p => p.Url)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });

        builder.HasMany(v => v.Pets)
            .WithOne();
    }
}