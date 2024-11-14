using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace PetVolunteer.Application.Database;
//TODO: Есть UnitOfWork, мб удалить или подумать над целесообразностью ApplicationDbContext в слое Application
public interface IApplicationDbContext
{
    DbSet<Domain.PetManagement.Volunteer.Entities.Volunteer> Volunteers { get; set; }
    DatabaseFacade Database { get; }
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}