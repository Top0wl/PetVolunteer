using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Providers.FileProvider;


//public record FilesUploadInfo(IEnumerable<FileUploadInfo> Files, string BucketName);

public record FileUploadInfo(Stream Stream, FilePath ObjectName, string BucketName);