using Azure.Storage.Blobs;
using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSkillsDevelopment.Core.Services
{
    public class FileService : IFileService
    {

        public async Task<string> SaveFile(StorageSettingsModel settings, IFormFile file)
        {

            if (settings.Option == "BlobStorage")
            {
                var path = await SaveToBlobStorage(file, settings.BlobConnectionStrings);
                return path;
            }
            else if (settings.Option == "FileSystem")
            {
                var path = SaveToFileSystem(file, settings.FileSystemPath);
                return path;
            }
            else
            {
                var path = SaveToDatabase(file);
                return path;
            }
        }

        private async Task<string> SaveToBlobStorage(IFormFile file, string? connectionString)
        {
            string containerName = "profileimages";
            BlobContainerClient blobClientContainer = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobClientContainer.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            var path = blobClient.Uri.AbsoluteUri;
            return path;
        }   
        private string SaveToFileSystem(IFormFile file, string? fileSystemPath)
        {
            var path = "Path from file system";
            return path;
        }   
        private string SaveToDatabase(IFormFile file)
        {
            var path = "Saved to DB";
            return path;
        }
    }
}
