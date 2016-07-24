using Microsoft.AspNetCore.Mvc;
using SampleFive.ServiceLayer;
using Microsoft.AspNetCore.Http;

namespace SampleFive.Features.Home
{
    public class HomePOCOController
    {
        [ActionContext]
        public ActionContext ActionContext { get; set; }
        public HttpContext HttpContext => ActionContext.HttpContext;


        private readonly IMessagesSampleService _messagesService;
        public HomePOCOController(IMessagesSampleService messagesService)
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
            return new ViewResult
            {
                ViewName = "IndexWithView"
            };
        }
        public string Index()
        {
            return _messagesService.GetSiteName2();
        }
    }
}
