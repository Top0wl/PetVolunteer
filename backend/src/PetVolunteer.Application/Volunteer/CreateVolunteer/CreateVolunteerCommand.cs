using FluentValidation;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Volunteer.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    string PhoneNumber,
    int ExperienceOfWork);