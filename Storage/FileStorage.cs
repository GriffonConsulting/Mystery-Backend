using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;

namespace Storage
{
    public class FileStorage : IFileStorage
    {
        private readonly string endpoint;
        private readonly string accessKey;
        private readonly string secretKey;
        private readonly string bucketName;
        private readonly string authenticationRegion;
        private readonly HttpClient httpClient;
        private readonly IMinioClient minioClient;

        public FileStorage(IConfiguration configuration, HttpClient httpClient)
        {
            endpoint = configuration["FileStorage:Endpoint"] ?? throw new Exception("configuration[\"FileStorage:Endpoint\"] not found");
            accessKey = configuration["FileStorage:AccessKey"] ?? throw new Exception("configuration[\"FileStorage:AccessKey\"] not found");
            secretKey = configuration["FileStorage:SecretKey"] ?? throw new Exception("configuration[\"FileStorage:SecretKey\"] not found");
            bucketName = configuration["FileStorage:BucketName"] ?? throw new Exception("configuration[\"FileStorage:BucketName\"] not found"); ;
            authenticationRegion = configuration["FileStorage:AuthenticationRegion"] ?? throw new Exception("configuration[\"FileStorage:AuthenticationRegion\"] not found");

            this.httpClient = httpClient;
            this.minioClient = new MinioClient().WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithRegion(authenticationRegion)
            .WithSSL()
            .Build();
        }

        public async Task UploadFileFromUrlAsync(string fileUrl, string obj)
        {
            using var response = await httpClient.GetAsync(fileUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erreur lors du téléchargement : {response.StatusCode}");

            await using var memoryStream = new MemoryStream();
            await response.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            var args = new PutObjectArgs().WithBucket(bucketName).WithObject(obj).WithStreamData(memoryStream).WithObjectSize(memoryStream.Length);
            await minioClient.PutObjectAsync(args);
        }

        public async Task<string> GetFileAsync(string obj)
        {
            var args = new PresignedGetObjectArgs().WithBucket(bucketName).WithObject(obj).WithExpiry(600);
            return await minioClient.PresignedGetObjectAsync(args);
        }
    }
}