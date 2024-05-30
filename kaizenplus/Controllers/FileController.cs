using System.IO;
using Microsoft.AspNetCore.Mvc;
using kaizenplus.Files;
using kaizenplus.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using kaizenplus.Attributes;
using kaizenplus.Enums;

namespace kaizenplus.Controllers
{
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileManager fileManager;
        private readonly IConfiguration configuration;

        public FileController(IFileManager fileManager, IConfiguration configuration)
        {
            this.fileManager = fileManager;
            this.configuration = configuration;
        }
        [AppAuthorize(Roles = new[] { Roles.Admin })]
        [HttpGet("logger")]
        public BaseResponse<string> Getlogger()
        {
            using (FileStream fs = new FileStream(@configuration.GetValue<string>("LoggerPath"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader rr = new StreamReader(fs))
            {
                return new BaseResponse<string>(rr.ReadToEnd());
            } 
            //return new BaseResponse<string>("");

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