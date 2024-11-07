using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using Constants = PetVolunteer.Domain.Shared.Constants;

namespace PetVolunteer.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        /*
         builder.Property(v => v.SocialMediaList)
            .HasConversion(
                list => JsonSerializer.Serialize(list, JsonSerializerOptions.Default),
                value => JsonSerializer.Deserialize<SocialMediaList>(value, JsonSerializerOptions.Default));
        */
        
        builder.ComplexProperty(v => v.FullName, vb =>
        {
            vb.Property(fullName => fullName.FirstName)
                .IsRequired()
                .HasColumnName("firstname");
            vb.Property(fullName => fullName.LastName)
                .IsRequired()
                .HasColumnName("lastname");
            vb.Property(fullName => fullName.Patronymic)
                .HasColumnName("patronymic");
        });
        
        builder.ComplexProperty(v => v.Email, vb =>
        {
            vb.Property(email => email.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
        });

        builder.Property(v => v.Description)
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        
        builder.ComplexProperty(v => v.PhoneNumber, vb =>
        {
            vb.Property(number => number.Value)
                .IsRequired()
                .HasColumnName("phone_number");
        });
        
        builder.ComplexProperty(v => v.Experience, vb =>
        {
            vb.Property(exp => exp.Value)
                .IsRequired()
                .HasColumnName("experience");
        });
        
        builder.OwnsOne(p => p.RequisitesList, r =>
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
        
        builder.OwnsOne(p => p.SocialMediaList, sm =>
        {
            sm.ToJson("social_media");
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
            .WithOne()
            .HasForeignKey("pet_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}