using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFive.Web.StartupCustomizations
{
    public abstract class MyCustomBaseView<TModel> : RazorPage<TModel>
    {
        //روش خاص تزریق وابستگی‌ها در فایل ویژه‌ی جاری
        [RazorInject]
        public IHtmlLocalizerFactory MyHtmlLocalizerFactory { get; set; }

        public bool IsRtlMyCultureSiteStyle() {
           var cr= CultureInfo.CurrentCulture.Name;
            bool isRtl;
            switch (cr)
            {
                case "en-US":
                    {
                        isRtl = false;
                    }
                    break;
                case "fa-IR":
                    {
                        isRtl = true;
                    }
                    break;
                default:
                    {
                        isRtl = false;
                    }
                    break;
            }
            return isRtl;
        }

        public IHtmlLocalizer MySharedLocalizer => MyHtmlLocalizerFactory.Create(
            baseName: "SharedResource" /*مشخصات*/,
            location: "SampleFive.ExternalResources" /*نام اسمبلی ثالث*/);

        public bool IsAuthenticated()
        {
            return Context.User.Identity.IsAuthenticated;
        }

#pragma warning disable 1998
        public override async Task ExecuteAsync()
        {
        }
#pragma warning restore 1998
    }
}
