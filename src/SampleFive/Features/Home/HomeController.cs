using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer;
using Microsoft.AspNetCore.Http;

namespace SampleFive.Features.Home
{
    public class HomeController : Controller
    {
        
        private readonly IMessagesSampleService _messagesService;
        public HomeController(IMessagesSampleService messagesService)
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
