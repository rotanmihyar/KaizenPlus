using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace kaizenplus.Localizations
{
    public class JsonStringLocalizerBase
    {
        protected readonly LocalizerCacheHelper _memCache;
        protected readonly IOptions<JsonLocalizationOptions> _localizationOptions;
        protected readonly string _baseName;
        protected readonly TimeSpan _memCacheDuration;

        protected const string CACHE_KEY = "LocalizationBlob";
        protected string resourcesRelativePath;
        protected string currentCulture = string.Empty;
        protected ConcurrentDictionary<string, string> localization;

        public JsonStringLocalizerBase(IOptions<JsonLocalizationOptions> localizationOptions, string baseName = null)
        {
            _baseName = CleanBaseName(baseName);
            _localizationOptions = localizationOptions;

            _memCache = new LocalizerCacheHelper(_localizationOptions.Value.Caching);

            _memCacheDuration = _localizationOptions.Value.CacheDuration;
        }

        protected string GetCacheKey(CultureInfo ci) => $"{CACHE_KEY}_{ci.Name}";

        private void SetCurrentCultureToCache(CultureInfo ci) => currentCulture = ci.Name;

        protected bool IsCultureCurrentCulture(CultureInfo ci)
        {
            return string.Equals(currentCulture, ci.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        protected void GetCultureToUse(CultureInfo cultureToUse)
        {
            if (_memCache.TryGetValue(GetCacheKey(cultureToUse), out localization))
            {
                SetCurrentCultureToCache(cultureToUse);
                return;
            }

            if (_memCache.TryGetValue(GetCacheKey(cultureToUse.Parent), out localization))
            {
                SetCurrentCultureToCache(cultureToUse.Parent);
                return;
            }

            if (_memCache.TryGetValue(GetCacheKey(_localizationOptions.Value.DefaultCulture), out localization))
            {
                SetCurrentCultureToCache(_localizationOptions.Value.DefaultCulture);
            }
        }

        protected void InitJsonStringLocalizer()
        {
            AddMissingCultureToSupportedCulture(CultureInfo.CurrentUICulture);
            AddMissingCultureToSupportedCulture(_localizationOptions.Value.DefaultCulture);
            if(_localizationOptions.Value.SupportedCultureInfos!=null)
            foreach (CultureInfo ci in _localizationOptions.Value.SupportedCultureInfos)
            {
                InitJsonStringLocalizer(ci);
            }

            GetCultureToUse(CultureInfo.CurrentUICulture);
        }

        protected void AddMissingCultureToSupportedCulture(CultureInfo cultureInfo)
        {
            return;
            if (!_localizationOptions.Value.SupportedCultureInfos.Contains(cultureInfo))
            {
                _ = _localizationOptions.Value.SupportedCultureInfos.Add(cultureInfo);
            }
        }

        protected void InitJsonStringLocalizer(CultureInfo currentCulture)
        {
            if (!_memCache.TryGetValue(GetCacheKey(currentCulture), out localization))
            {
                ConstructLocalizationObject(resourcesRelativePath, currentCulture);

                _memCache.Set(GetCacheKey(currentCulture), localization, _memCacheDuration);
            }
        }

        private void ConstructLocalizationObject(string jsonPath, CultureInfo currentCulture)
        {
            if (localization == null)
            {
                localization = new ConcurrentDictionary<string, string>();
            }

            IEnumerable<string> myFiles = GetMatchingJsonFiles(jsonPath);

            foreach (string file in myFiles)
            {
                var splitResult = Path.GetFileName(file).Split('.');
                if (splitResult.Length >= 3)
                {
                    if (splitResult[splitResult.Length - 2] == currentCulture.Name)
                    {
                        ConcurrentDictionary<string, string> tempLocalization = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(File.ReadAllText(file, _localizationOptions.Value.FileEncoding));
                        if (tempLocalization == null)
                        {
                            continue;
                        }
                        foreach (KeyValuePair<string, string> temp in tempLocalization)
                        {
                            string localizedValue = GetLocalizedValue(temp);
                            if (!localization.ContainsKey(splitResult[0] + '_' + temp.Key))
                            {
                                localization.TryAdd(splitResult[0] + '_' + temp.Key, localizedValue);
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<string> GetMatchingJsonFiles(string jsonPath)
        {
            string searchPattern = "*.json";
            SearchOption searchOption = SearchOption.AllDirectories;
            string basePath = jsonPath;
            const string sharedSearchPattern = "*.shared.json";
            List<string> files = new List<string>();
            if (_localizationOptions.Value.UseBaseName && !string.IsNullOrWhiteSpace(_baseName))
            {
                searchOption = SearchOption.TopDirectoryOnly;
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;

                string shortName = _baseName.Replace($"{friendlyName}.", "");

                basePath = Path.Combine(jsonPath, TransformNameToPath(shortName));
                if (Directory.Exists(basePath))
                {
                    searchPattern = "*.json";
                }
                else
                {
                    int lastDot = shortName.LastIndexOf('.');
                    string className = shortName.Substring(lastDot + 1);
                    string baseFolder = shortName.Substring(0, lastDot);
                    baseFolder = TransformNameToPath(baseFolder);

                    basePath = Path.Combine(jsonPath, baseFolder);

                    if (Directory.Exists(basePath))
                    {
                        searchPattern = $"{className}?.json";
                    }
                    else
                    {
                        basePath = jsonPath;
                        searchPattern = $"{shortName}?.json";
                    }
                }

                files = Directory.GetFiles(basePath, searchPattern, searchOption).ToList();
                files.AddRange(Directory.GetFiles(basePath, sharedSearchPattern, SearchOption.TopDirectoryOnly));
                files.AddRange(Directory.GetFiles(jsonPath, $"localization.shared.json", SearchOption.TopDirectoryOnly));
            }
            else
            {
                files = Directory.GetFiles(basePath, searchPattern, searchOption).ToList();
            }

            return files;
        }

        private string TransformNameToPath(string name)
        {
            return !string.IsNullOrEmpty(name) ? name.Replace(".", Path.DirectorySeparatorChar.ToString()) : null;
        }

        private string CleanBaseName(string baseName)
        {
            if (!string.IsNullOrEmpty(baseName))
            {
                int plusIdx = baseName.IndexOf('+');
                return plusIdx == -1 ? baseName : baseName.Substring(0, plusIdx);
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetLocalizedValue(KeyValuePair<string, string> temp)
        {
            return temp.Value;
        }
    }
}