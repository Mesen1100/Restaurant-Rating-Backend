using CleanArchitecture.Core.Features.Categories.Commands.CreateCategory;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController:ControllerBase
    {
        private readonly IBlobStorageService _blobStorage;

        public FileController(IBlobStorageService blobStorageService)
        {
            _blobStorage = blobStorageService;
        }

        [Authorize]
        [HttpPost]
        public async Task<string> Upload(IFormFile file)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid").Value;
            return await _blobStorage.Upload(file, userId);
        }

        [HttpGet("listBlobs")]
        public async Task<List<string>> ListBlobs()
        {
            return await _blobStorage.ListBlobs();
        }

        [HttpGet("listContainers")]
        public async Task<List<string>> ListContainer()
        {
            return await _blobStorage.ListContainers();
        }
    }
}
