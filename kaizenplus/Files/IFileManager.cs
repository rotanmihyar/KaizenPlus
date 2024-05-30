using Microsoft.AspNetCore.Http;
using kaizenplus.Attributes;

namespace kaizenplus.Files
{
    [ScopedInjectable]
    public interface IFileManager
    {
        string Save(byte[] file, string extension);

        string Save(IFormFile file);

        string Copy(string name);

        void Delete(string name);

        byte[] Get(string name);

        bool Exists(string name);

        string GetBase64Extension(string data);
    }
}