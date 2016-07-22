using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer;
using Microsoft.AspNetCore.Http;

namespace SampleFive.Features.Home
{
    public class HomeController : Controller
    {
        
        private readonly IMessagesService _messagesService;
        public HomeController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        public IActionResult About()
        {
            return new ContentResult
            {
                Content = "Hello my King!",
                ContentType = "text/plain; charset=utf-8"
            };
        }

        public IActionResult IndexWithView()
        {
            return View();
        }
        public string Index()
        {
            return _messagesService.GetSiteName2();
        }
    }
}
