using PetVolunteer.Application.Authorization.DataModels;

namespace PetVolunteer.Application.Authorization;

public interface ITokenProvider
{
    string GenerateAccessToken(User user);
}