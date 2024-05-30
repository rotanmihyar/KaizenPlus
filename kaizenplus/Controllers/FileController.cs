using System.IO;
using Microsoft.AspNetCore.Mvc;
using kaizenplus.Files;

namespace kaizenplus.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileManager fileManager;

        public FileController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var content = new MemoryStream(fileManager.Get(name));
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, name);
        }
    }
}