using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace SampleFive.Web.StartupCustomizations
{
    public class FeatureLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(FeatureLocationExpander);
        }

        public IEnumerable<string> ExpandViewLocations(
            ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[]
            {
                "/Features/{1}/{0}.cshtml",
                "/Features/Shared/{0}.cshtml"
            };
        }
    }
}
