using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;

namespace Relive.Server.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        readonly IFileProvider _fileProvider;
        public ImageController(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Route("{UserId}/{Folder}/{Id}")]
        public IActionResult GetImage(string UserId, string Folder, string Id)
        {
            var fileInfo = _fileProvider.GetFileInfo($"{UserId}\\{Folder}\\{Id}.jpg");
            if (!fileInfo.Exists)
            {
                return NoContent();
            }
            var file = fileInfo.CreateReadStream();
            return File(file, "image/jpeg");
        }
    }
}
