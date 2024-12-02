using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(p => p.Id);

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId);

        builder.Property(i => i.Requisites)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<RequisiteDto[]>(json, JsonSerializerOptions.Default)!);
        
        builder.Property(i => i.SocialMedia)
            .HasConversion(
                requisites => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<SocialMediaDto[]>(json, JsonSerializerOptions.Default)!);
    }
}