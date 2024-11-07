namespace PetVolunteer.Application.Providers.FileProvider;

public class FileDownloadInfo
{
    public string BucketName { get; }
    public string ObjectName { get; }

    public FileDownloadInfo(string bucketName, string objectName)
    {
        BucketName = bucketName ?? throw new ArgumentNullException(nameof(bucketName));
        ObjectName = objectName ?? throw new ArgumentNullException(nameof(objectName));
    }
}