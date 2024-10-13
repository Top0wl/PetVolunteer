using FluentValidation;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Volunteer.CreateVolunteer;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string email,
    string description,
    string phoneNumber,
    int experienceOfWork);