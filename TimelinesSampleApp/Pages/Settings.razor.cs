using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DevInstance.Timeline.Sample.Pages
{
    public partial class Settings
    {
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public class LanguageItem
        {
            public CultureInfo Culture { get; set; }
            public string FlagName { get; set; }
        }

        LanguageItem[] SupportedLanguages;

        CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (CultureInfo.CurrentCulture != value)
                {
                    var js = (IJSInProcessRuntime)JSRuntime;
                    js.InvokeVoid("blazorCulture.set", value.Name);
                    NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
                }
            }
        }

        protected async override Task OnInitializedAsync()
        {
            SupportedLanguages = new[]
                        {
                        CreateLanguageItem("ar-SA", "\U0001f1f8\U0001f1e6"),
                        CreateLanguageItem("bg-BG", "\U0001f1e7\U0001f1ec"),
                        CreateLanguageItem("ca-ES", ""),
                        CreateLanguageItem("zh-TW", ""),
                        CreateLanguageItem("ja-JP", "\U0001f1ef\U0001f1f5"),
                        CreateLanguageItem("de-DE", "\U0001f1e9\U0001f1ea"),
                        CreateLanguageItem("en-US", "\U0001f1fa\U0001f1f8"),
                        CreateLanguageItem("uk-UA", "\U0001f1fa\U0001f1e6")
                        };
        }

        private LanguageItem CreateLanguageItem(string culture, string flagName)
        {
            return new LanguageItem
            {
                Culture = new CultureInfo(culture),
                FlagName = flagName
            };
        }

    }
}
