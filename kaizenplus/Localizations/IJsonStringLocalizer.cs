using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace kaizenplus.Localizations
{
    public interface IJsonStringLocalizer : IStringLocalizer
    {
        void ClearMemCache(IEnumerable<CultureInfo> culturesToClearFromCache = null);
        LocalizedString GetString(string name, string type);
        LocalizedString GetString(string name, params string[] parameters);
        LocalizedString GetString(string name, string type, params string[] parameters);
    }

    public interface IJsonStringLocalizer<T> : IJsonStringLocalizer
    {

    }
}