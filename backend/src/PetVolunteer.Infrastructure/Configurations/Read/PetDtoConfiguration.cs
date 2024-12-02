using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetVolunteer.Application.DTOs;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;

namespace PetVolunteer.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(p => p.Id);
            
    }
}