using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer;

namespace SampleFive.ViewComponents
{
    public class SiteCopyright : ViewComponent
    {
        private readonly IMessagesSampleService _messagesService;

        public SiteCopyright(IMessagesSampleService messagesService)
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