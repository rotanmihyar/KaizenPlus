using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using kaizenplus.Files.Models;

namespace kaizenplus.Files
{
    public class FileManager : IFileManager
    {
        private readonly FileManagerConfigurations configurations;

        public FileManager(IOptions<FileManagerConfigurations> configurations)
        {
            this.configurations = configurations.Value;

            Directory.CreateDirectory(this.configurations.Path);
        }

        public bool Exists(string name)
        {
            return File.Exists(configurations.Path + name);
        }

        public byte[] Get(string name)
        {
            if (Exists(name))
            {
                var file = File.ReadAllBytes(configurations.Path + name);
                return file;
            }

            return new byte[] { };
        }

     
        public string Save(byte[] file, string extension)
        {
            var fileName = Guid.NewGuid().ToString();
            fileName += $".{extension}";

            File.WriteAllBytes(configurations.Path + fileName, file);
            return fileName;
        }

        public string Save(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    var extension = file.FileName.Split('.').Last();

                    return Save(fileBytes, extension);
                }
            }

            return "";
        }

        public string Copy(string name)
        {
            var file = Get(name);

            return Save(file, name.Split('.').Last());
        }

        public void Delete(string name)
        {
            File.Delete(configurations.Path + name);
        }
    }
}