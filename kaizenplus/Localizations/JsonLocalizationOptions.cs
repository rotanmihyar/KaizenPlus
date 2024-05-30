﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace kaizenplus.Localizations
{
    public class JsonLocalizationOptions : LocalizationOptions
    {
        private const char PLURAL_SEPARATOR = '|';
        private const string DEFAULT_RESOURCES = "Resources";
        private const string DEFAULT_CULTURE = "en-US";

        public new string ResourcesPath { get; set; } = DEFAULT_RESOURCES;

        public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(30);

        public IMemoryCache Caching { get; set; } = new MemoryCache(new MemoryCacheOptions
        {
        });

        private CultureInfo defaultCulture = new CultureInfo(DEFAULT_CULTURE);

        public CultureInfo DefaultCulture
        {
            get => defaultCulture;
            set
            {
                if (value != defaultCulture)
                {
                    defaultCulture = value ?? CultureInfo.InvariantCulture;
                }
            }
        }

        private HashSet<CultureInfo> supportedCultureInfos = new HashSet<CultureInfo>
        {

        };

        public HashSet<CultureInfo> SupportedCultureInfos
        {
            get
            {

                var languageHelper = AppHttpContext.GetService<ILanguageHelper>();
                return null; //languageHelper.SupportedCultureInfos;
            }
        }

        public bool IsAbsolutePath { get; set; } = false;

        public Encoding FileEncoding { get; set; } = Encoding.UTF8;

        public bool UseBaseName { get; set; } = false;

        public char PluralSeparator { get; set; } = PLURAL_SEPARATOR;
    }
}