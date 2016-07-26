using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFive.Web.ViewComponents
{
    public class SiteWelcome : ViewComponent
    {
        private readonly IMessagesSampleService _messagesService;

        public SiteWelcome(IMessagesSampleService messagesService)
        {
            _messagesService = messagesService;
        }

        public IViewComponentResult Invoke()
        {
            var name = _messagesService.GetSiteWelcome();
            return View(viewName: "Default", model: name);
        }

        //public async Task<IViewComponentResult> InvokeAsync(int numberToTake)
        //{
        //    return View();
        //}
    }
}
