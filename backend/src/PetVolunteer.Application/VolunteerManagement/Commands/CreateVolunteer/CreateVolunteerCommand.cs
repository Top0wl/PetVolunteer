using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.VolunteerManagement.Commands.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    string Email,
    string Description,
    string PhoneNumber,
    int ExperienceOfWork) : ICommand;