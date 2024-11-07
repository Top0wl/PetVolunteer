using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Providers.FileProvider;

public interface IFileProvider
{
    Task<Result<IReadOnlyList<FilePath>, Error>> UploadAsync(
        IEnumerable<FileUploadInfo> filesUploadInfo,
        CancellationToken cancellationToken = default);

    Task<Result<string, Error>> GetAsync(
        FileDownloadInfo fileDownloadInfo,
        CancellationToken cancellationToken = default);
}