
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchitecture.Core.Interfaces
{
    public interface IBlobStorageService
    {
        public Task<string> Upload(IFormFile file, string userId);
        public Task<List<string>> ListBlobs();
        public Task<List<string>> ListContainers();
    }
}
