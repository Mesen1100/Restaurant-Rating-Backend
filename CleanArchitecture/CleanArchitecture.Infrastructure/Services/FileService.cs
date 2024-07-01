using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private string _credentialspath { get; set; }
        private DriveService _driveService { get; set; }
        public FileService()
        {
            _credentialspath= "credentials.json";
            _driveService=AuthenticateServiceAccount(_credentialspath);

        }
        //This code make authenticate to google drive api
        public static DriveService AuthenticateServiceAccount(string credentialsPath)
        {
            GoogleCredential credential;

            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.ScopeConstants.DriveFile);
            }

            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveFileUpload",
            });
        }
        //This method return file id
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            try
            {
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = fileName
                };

                FilesResource.CreateMediaUpload request;
                request = _driveService.Files.Create(fileMetadata, fileStream, GetMimeType(fileName));
                request.Fields = "id";
                await request.UploadAsync();

                var file = request.ResponseBody;

                // Set file permissions
                await SetFilePublic(file.Id);

                return file.Id;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        //This method make file public to access from frontend
        private async Task SetFilePublic(string fileId)
        {
            var permission = new Google.Apis.Drive.v3.Data.Permission()
            {
                Role = "reader",
                Type = "anyone"
            };

            var request = _driveService.Permissions.Create(permission, fileId);
            request.Fields = "id";
            await request.ExecuteAsync();
        }
        //This method make link accesseble to picture
        public string GetPictureLink(string fileId)
        {
            return $"https://drive.google.com/thumbnail?id={fileId}";
        }
        private static string GetMimeType(string fileName)
        {
            var mimeTypes = new Dictionary<string, string>
    {
        { ".bmp", "image/bmp" },
        { ".gif", "image/gif" },
        { ".ico", "image/vnd.microsoft.icon" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".png", "image/png" },
        { ".svg", "image/svg+xml" },
        { ".tiff", "image/tiff" },
        { ".tif", "image/tiff" },
        { ".webp", "image/webp" },
        { ".heic", "image/heic" },
        { ".heif", "image/heif" },
        { ".jfif", "image/jpeg" },
        { ".pjpeg", "image/pjpeg" },
        { ".pjp", "image/pjp" },
        { ".avif", "image/avif" }
    };

            string ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (!mimeTypes.TryGetValue(ext, out string mimeType))
            {
                throw new FileNotPictureException(fileName);
            }
            return mimeType;
        }
        public async Task<IList<Google.Apis.Drive.v3.Data.File>> ListFilesAsync()
        {
            var request = _driveService.Files.List();
            request.Fields = "nextPageToken, files(id, name)";

            var result = await request.ExecuteAsync();
            return result.Files;
        }
        public async Task<string> DeleteFileAsync(string fileId)
        {
            try
            {
                var request = _driveService.Files.Delete(fileId);
                await request.ExecuteAsync();
                return ($"File with ID: {fileId} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return ($"Error deleting file: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if necessary
            }
        }
        public async Task<IDictionary<string, string>> ListFileNamesAndIdsAsync()
        {
            var request = _driveService.Files.List();
            request.Fields = "nextPageToken, files(id, name)";

            var result = await request.ExecuteAsync();

            var fileDict = new Dictionary<string, string>();
            foreach (var file in result.Files)
            {
                var fileName = file.Name;
                if (fileDict.ContainsKey(fileName))
                {
                    int count = 1;
                    string newFileName;
                    do
                    {
                        newFileName = $"{fileName} ({count})";
                        count++;
                    } while (fileDict.ContainsKey(newFileName));
                    fileDict[newFileName] = file.Id;
                }
                else
                {
                    fileDict[fileName] = file.Id;
                }
            }

            return fileDict;
        }
    }
}
