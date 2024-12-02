using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Infrastructure.Configurations.Write;

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
                .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
        });

        builder.Property(v => v.Description)
            .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);

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

        builder.OwnsMany(
            v => v.Requisites,
            rb =>
            {
                rb.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);

                rb.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
                rb.ToJson("requisites");
            });

        builder.OwnsMany(
            v => v.SocialMedia,
            smb =>
            {
                smb.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
                smb.Property(p => p.Url)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
                smb.ToJson("social_media");
            });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}

/*builder.OwnsOne(v => v.Requisites, r =>
        {
            r.ToJson("requisites");
            r.OwnsMany(rl => rl.Values, brl =>
            {
                brl.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
                brl.Property(p => p.Description)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });

        builder.OwnsOne(v => v.SocialMedia, sm =>
        {
            sm.ToJson("social_media");
            sm.OwnsMany(sml => sml.Values, bsml =>
            {
                bsml.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_LOW_TEXT_LENGTH);
                bsml.Property(p => p.Url)
                    .IsRequired()
                    .HasMaxLength(Domain.Shared.Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });
        */