using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Reflection.Extensions;

namespace SimpleTaskApp.Localization
{
    public static class SimpleTaskAppLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flags england"));
            localizationConfiguration.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flags tr"));
            localizationConfiguration.Languages.Add(new LanguageInfo("zh-cn","中文简体", "famfamfam-flags cn", isDefault:true));

            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SimpleTaskAppConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SimpleTaskAppLocalizationConfigurer).GetAssembly(),
                        "SimpleTaskApp.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}