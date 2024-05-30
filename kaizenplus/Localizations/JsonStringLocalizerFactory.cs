using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace kaizenplus.Localizations
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IOptions<JsonLocalizationOptions> _localizationOptions;

        public JsonStringLocalizerFactory(
                IWebHostEnvironment webHostEnvironment,
                IOptions<JsonLocalizationOptions> localizationOptions = null)
        {
            this.webHostEnvironment = webHostEnvironment;
            _localizationOptions = localizationOptions ?? throw new ArgumentNullException(nameof(localizationOptions));
        }


        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_localizationOptions, webHostEnvironment);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            baseName = _localizationOptions.Value.UseBaseName ? baseName : string.Empty;
            return new JsonStringLocalizer(_localizationOptions, webHostEnvironment, baseName);
        }
    }
}