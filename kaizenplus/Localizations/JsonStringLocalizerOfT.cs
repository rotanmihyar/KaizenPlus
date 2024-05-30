using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace kaizenplus.Localizations
{
    public class JsonStringLocalizerOfT<T> : JsonStringLocalizer, IJsonStringLocalizer<T>, IStringLocalizer<T>
    {
        public JsonStringLocalizerOfT(IOptions<JsonLocalizationOptions> localizationOptions, IWebHostEnvironment env, string baseName = null) : base(localizationOptions, env, baseName)
        {
        }
    }
}