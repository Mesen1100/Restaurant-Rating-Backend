using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        string GetPictureLink(string fileId);
        Task<string> DeleteFileAsync(string fileId);
        Task<IList<Google.Apis.Drive.v3.Data.File>> ListFilesAsync();
        Task<IDictionary<string, string>> ListFileNamesAndIdsAsync();
    }
}
