namespace PetVolunteer.Domain.Volunteer.Entities;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}