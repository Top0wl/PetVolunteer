using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Providers.FileProvider;

public record FileUploadInfo(Stream Stream, FilePath ObjectName, string BucketName);