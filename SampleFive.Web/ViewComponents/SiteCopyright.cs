using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer.Interfaces;

namespace SampleFive.Web.ViewComponents
{
    public class SiteCopyright : ViewComponent
    {
        private readonly ISettingService _messagesService;

        public SiteCopyright(ISettingService messagesService)
        {
            _messagesService = messagesService;
        }

        public IViewComponentResult Invoke(int numberToTake)
        {
            var name = _messagesService.GetSiteCopyRightConfig();
            return View(viewName: "Default", model: name);
        }

        //public async Task<IViewComponentResult> InvokeAsync(int numberToTake)
        //{
        //    return View();
        //}
    }
}
