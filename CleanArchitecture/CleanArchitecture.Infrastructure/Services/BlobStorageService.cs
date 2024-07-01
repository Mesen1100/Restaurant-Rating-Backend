using Azure.Storage.Blobs;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class BlobStorageService:IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobStorageConnectionString;
        private readonly BlobStorageSettings _blobStorageSettings;

        public BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettings)
        {
            _blobStorageConnectionString = blobStorageSettings.Value.ConnectionString;
            _blobServiceClient = new BlobServiceClient(_blobStorageConnectionString);
            _blobStorageSettings = blobStorageSettings.Value;

        }
        public async Task<string> Upload(IFormFile file, string userId)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);
            var blobName = $"{userId}\\" + file.FileName;
            var blob = blobContainer.GetBlobClient(blobName);
            int counter = 1;
            string newBlobName = blobName;
            while (await blob.ExistsAsync())
            {
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                string extension = System.IO.Path.GetExtension(file.FileName);
                newBlobName = $"{userId}\\" + fileNameWithoutExtension + $"({counter})" + extension;
                blob = blobContainer.GetBlobClient(newBlobName);
                counter++;
            }

            var stream = file.OpenReadStream();
            var result = await blob.UploadAsync(stream);
            return blob.Uri.ToString();

        }
        public async Task<List<string>> ListBlobs()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);
            var blobNames = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                blobNames.Add(blobItem.Name);
            }

            return blobNames;
        }
        public async Task<List<string>> ListContainers()
        {
            var containerNames = new List<string>();

            await foreach (var containerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerNames.Add(containerItem.Name);
            }

            return containerNames;
        }
    }
}
