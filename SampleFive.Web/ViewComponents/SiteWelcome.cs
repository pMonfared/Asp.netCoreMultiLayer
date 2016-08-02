using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer.Interfaces;

namespace SampleFive.Web.ViewComponents
{
    public class SiteWelcome : ViewComponent
    {
        private readonly ISettingService _messagesService;

        public SiteWelcome(ISettingService messagesService)
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
