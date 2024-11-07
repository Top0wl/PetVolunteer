using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetVolunteer.Application.Providers;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Infrastructure.Providers;

public class MinioProvider : IFileProvider
{
    private const int MAX_DEGREE_OF_PARALLELISM = 5;
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadAsync(
        IEnumerable<FileUploadInfo> filesUploadInfo,
        CancellationToken cancellationToken = default)
    {
        var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);
        var filesList = filesUploadInfo.ToList();

        try
        {
            await IfBucketsNotExistsCreateBucket(filesList, cancellationToken);

            var tasks = filesList.Select(async file => await PutObject(file, semaphoreSlim, cancellationToken));

            var pathsResult = await Task.WhenAll(tasks);

            if (pathsResult.Any(p => p.IsFailure))
                return pathsResult.First().Error;

            var results = pathsResult.Select(p => p.Value).ToList();

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload files in minio, files amount: {amount}", filesList.Count);
            return Error.Failure("file.upload", "Fail to upload files in minio");
        }
    }

    private async Task IfBucketsNotExistsCreateBucket(
        List<FileUploadInfo> filesList,
        CancellationToken cancellationToken)
    {
        HashSet<string> bucketNames = filesList.Select(file => file.BucketName).ToHashSet();

        foreach (var bucketName in bucketNames)
        {
            var bucketExistsArgs = new BucketExistsArgs()
                .WithBucket(bucketName);

            var isBucketExists = await _minioClient
                .BucketExistsAsync(bucketExistsArgs, cancellationToken);

            if (isBucketExists == false)
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
            }
        }
    }

    private async Task<Result<FilePath, Error>> PutObject(
        FileUploadInfo fileUploadInfo, SemaphoreSlim semaphoreSlim, CancellationToken cancellationToken)
    {
        await semaphoreSlim.WaitAsync(cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(fileUploadInfo.BucketName)
            .WithStreamData(fileUploadInfo.Stream)
            .WithObjectSize(fileUploadInfo.Stream.Length)
            .WithObject(fileUploadInfo.ObjectName.ObjectName);

        try
        {
            await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
            return fileUploadInfo.ObjectName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to upload file in minio with objectName {objectName} in bucket {bucket}", 
                fileUploadInfo.ObjectName,
                fileUploadInfo.BucketName);
            return Error.Failure("file.upload", "Fail to upload file");
        }
        finally
        {
            semaphoreSlim.Release();
        }
        //10:49
    }


    /*
    try
    {
        //Проверка что Bucket существует
        var bucketExistArgs = new BucketExistsArgs()
            .WithBucket(filesUploadInfo.BucketName);
        var bucketExistResult = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
        if (bucketExistResult == false)
        {
            var makeBucketArgs = new MakeBucketArgs()
                .WithBucket(filesUploadInfo.BucketName);
            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }

        List<Task> tasks = [];
        foreach (var fileUploadInfo in filesUploadInfo.Files)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);

            //Загрузка файла
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(filesUploadInfo.BucketName)
                .WithStreamData(fileUploadInfo.Stream)
                .WithObjectSize(fileUploadInfo.Stream.Length)
                .WithObject(fileUploadInfo.ObjectName);

            var task = _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            semaphoreSlim.Release();

            tasks.Add(task);
        }

        await Task.WhenAll(tasks);

    }
    catch (Exception ex)
    {
        //Удаление лишних файлов
        _logger.LogError(ex, "Fail to upload file in minio");
        return Error.Failure("file.upload", "Fail to upload file in minio");
    }
    finally
    {
        semaphoreSlim.Release();
    }

    return Result.Success<Error>();
    */
    //}

    public async Task<Result<string, Error>> DeleteAsync(
        FileUploadInfo fileUploadInfo,
        CancellationToken cancellationToken = default)
    {
        return Errors.General.NotFound();
    }
    
    public async Task<Result<string, Error>> GetAsync(
        FileDownloadInfo fileDownloadInfo,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(fileDownloadInfo.BucketName)
                .WithObject(fileDownloadInfo.ObjectName)
                .WithExpiry(60 * 60 * 24);
            var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to download file in minio");
            return Error.Failure("file.download", "Fail to download file in minio");
        }
    }
}