using System.Data;

namespace PetVolunteer.Application.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}