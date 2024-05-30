using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace kaizenplus.Localizations
{
    public class JsonStringLocalizer : JsonStringLocalizerBase, IJsonStringLocalizer
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public JsonStringLocalizer(IOptions<JsonLocalizationOptions> localizationOptions, IWebHostEnvironment webHostEnvironment, string baseName = null) : base(localizationOptions, baseName)
        {
            this.webHostEnvironment = webHostEnvironment;
            resourcesRelativePath = GetJsonRelativePath(_localizationOptions.Value.ResourcesPath);

            InitJsonStringLocalizer();
        }

        public LocalizedString this[string name]
        {
            get
            {
                string value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                string format = GetString(name);
                string value = GetPluralLocalization(name, format, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        private string GetPluralLocalization(string name, string format, object[] arguments)
        {
            object last = arguments.LastOrDefault();
            string value;
            if (last != null && last is bool boolean)
            {
                bool isPlural = boolean;
                value = GetString(name);
                if (!string.IsNullOrEmpty(value) && value.Contains(_localizationOptions.Value.PluralSeparator))
                {
                    int index = isPlural ? 1 : 0;
                    value = value.Split(_localizationOptions.Value.PluralSeparator)[index];
                }
                else
                {
                    value = string.Format(format ?? name, arguments);
                }
            }
            else
            {
                value = string.Format(format ?? name, arguments);
            }

            return value;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return includeParentCultures ? localization?
                    .Select(
                        l =>
                        {
                            string value = GetString(l.Key);
                            return new LocalizedString(l.Key, value ?? l.Key, resourceNotFound: value == null);
                        }
                    ) :
                    localization?
                    .Select(
                        l =>
                        {
                            string value = GetString(l.Key);
                            return new LocalizedString(l.Key, value ?? l.Key, resourceNotFound: value == null);
                        }
                    ).OrderBy(s => s.Name);
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            if (!_localizationOptions.Value.SupportedCultureInfos.Contains(culture))
            {
                _localizationOptions.Value.SupportedCultureInfos.Add(culture);
            }

            CultureInfo.CurrentUICulture = culture;

            return new JsonStringLocalizer(_localizationOptions, webHostEnvironment);
        }

        private string GetString(string name, bool shouldTryDefaultCulture = true)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (shouldTryDefaultCulture && !IsCultureCurrentCulture(CultureInfo.CurrentUICulture))
            {
                InitJsonStringLocalizer(CultureInfo.CurrentUICulture);
                AddMissingCultureToSupportedCulture(CultureInfo.CurrentUICulture);
                GetCultureToUse(CultureInfo.CurrentUICulture);
            }


            if (localization != null && localization.TryGetValue(name, out string localizedValue))
            {
                return localizedValue;
            }

            if (shouldTryDefaultCulture)
            {
                GetCultureToUse(_localizationOptions.Value.DefaultCulture);
                return GetString(name, false);
            }

            Console.Error.WriteLine($"{name} does not contain any translation");
            return null;
        }

        public LocalizedString GetString(string name, string type)
        {
            switch (type)
            {
                default:
                    name = "General_" + name;
                    break;
            }

            string value = GetString(name);
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
        }

        public LocalizedString GetString(string name, params string[] parameters)
        {
            var value = GetString(name);
            return new LocalizedString(name, string.Format(value, parameters) ?? name, resourceNotFound: value == null);
        }

        public LocalizedString GetString(string name, string type, params string[] parameters)
        {
            var localizedString = GetString(name, type);
            if (!localizedString.ResourceNotFound)
            {
                return new LocalizedString(name, string.Format(localizedString.Value, parameters) ?? name, resourceNotFound: localizedString.ResourceNotFound);
            }
            return localizedString;
        }

        private string GetJsonRelativePath(string path)
        {
            string fullPath = string.Empty;
            if (_localizationOptions.Value.IsAbsolutePath)
            {
                fullPath = path;
            }
            if (!_localizationOptions.Value.IsAbsolutePath && string.IsNullOrEmpty(path))
            {
                fullPath = Path.Combine(webHostEnvironment.ContentRootPath, "Resources");
            }
            else if (!_localizationOptions.Value.IsAbsolutePath && !string.IsNullOrEmpty(path))
            {
                fullPath = Path.Combine(AppContext.BaseDirectory, path2: path);
            }
            return fullPath;
        }

        public void ClearMemCache(IEnumerable<CultureInfo> culturesToClearFromCache = null)
        {
            foreach (var cultureInfo in culturesToClearFromCache ??
                                         _localizationOptions.Value.SupportedCultureInfos.ToArray())
            {
                _memCache.Remove(GetCacheKey(cultureInfo));
            }
        }
    }
}