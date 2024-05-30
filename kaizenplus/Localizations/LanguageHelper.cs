using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using kaizenplus.Attributes;
using kaizenplus.Enums;

namespace kaizenplus.Localizations
{
    [SingeltonInjectable]
    public interface ILanguageHelper
    {
        void SetCurrentUICulture(string code);

        public int GetCurrentLanguageId();

        public string GetLanguageCode();

       // LanguageEnum GetLanguage(string code);

      //  string GetLanguageCode(LanguageEnum language);

       // Dictionary<LanguageEnum, string> SupportedLanguages { get; }

        public HashSet<CultureInfo> SupportedCultureInfos { get; }
    }

    //public class LanguageHelper : ILanguageHelper
    //{
    //    public Dictionary<LanguageEnum, string> SupportedLanguages
    //    {
    //        get
    //        {
    //            return new Dictionary<LanguageEnum, string>()
    //            {
    //                {LanguageEnum.Arabic,"ar" },
    //                {LanguageEnum.English,"en" }
    //            };
    //        }
    //    }

    //    public void SetCurrentUICulture(string code)
    //    {
    //        if (SupportedLanguages.Any(x => x.Value == code))
    //        {
    //            Thread.CurrentThread.CurrentUICulture = new CultureInfo(code);
    //        }
    //        else
    //        {
    //            code = SupportedLanguages.FirstOrDefault(x => x.Key == LanguageEnum.Arabic).Value;
    //            Thread.CurrentThread.CurrentUICulture = new CultureInfo(code);
    //        }
    //    }

    //    public string GetLanguageCode()
    //    {
    //        return Thread.CurrentThread.CurrentUICulture.Name;
    //    }

    //    public LanguageEnum GetLanguage(string code)
    //    {
    //        return SupportedLanguages.FirstOrDefault(x => x.Value == code).Key;
    //    }

    //    public int GetCurrentLanguageId()
    //    {
    //        return Convert.ToInt32(SupportedLanguages.FirstOrDefault(x => x.Value == Thread.CurrentThread.CurrentUICulture.Name).Key);
    //    }

    //    public string GetLanguageCode(LanguageEnum language)
    //    {
    //        return SupportedLanguages.FirstOrDefault(x => x.Key == language).Value;
    //    }

    //    public HashSet<CultureInfo> SupportedCultureInfos
    //    {
    //        get
    //        {
    //            HashSet<CultureInfo> supportedCultureInfos = new HashSet<CultureInfo>();
    //            foreach (var item in SupportedLanguages)
    //            {
    //                supportedCultureInfos.Add(new CultureInfo(item.Value));
    //            }
    //            return supportedCultureInfos;
    //        }
    //    }
    //}
}