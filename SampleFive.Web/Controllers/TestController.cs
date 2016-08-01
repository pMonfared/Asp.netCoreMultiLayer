using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleFive.DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using SampleFive.ServiceLayer.Interfaces;
using Microsoft.Extensions.Logging;
using SampleFive.PresentaionLayer.AccountViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleFive.Web.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;

        public TestController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _logger = loggerFactory.CreateLogger<TestController>();
        }
        // GET: api/values
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<ApplicationUser> GetList()
        {
            return _userManager.Users.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(int id)
        {
            var taskResult = _userManager.FindByIdAsync(id.ToString());
            if (taskResult.IsCompleted)
            {
                if (taskResult.Result == null)
                {
                    return NotFound();
                }
                return new ObjectResult(taskResult.Result);
            }
            else
            {
                return BadRequest();
            }
            
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]RegisterViewModel modelvm)
        {
            if (modelvm == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = modelvm.Email, Email = modelvm.Email, FirstName = modelvm.FirstName, LastName = modelvm.LastName };
                var result = await _userManager.CreateAsync(user, modelvm.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(modelvm.Email, "Confirm your account",
                    "Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return CreatedAtRoute("Get", new { id = user.Id }, user);
                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();
            }
            
        }

        

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromForm]RegisterViewModel modelvm)
        {
            //if (model == null || model.id != id)
            if (modelvm == null)
            {
                return BadRequest();
            }

            var taskResult = _userManager.FindByIdAsync(id.ToString());
            if (taskResult.Result == null)
            {
                return NotFound();
            }

            _userManager.UpdateAsync(taskResult.Result);
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskResult = _userManager.FindByIdAsync(id.ToString());
            if (taskResult.Result == null)
            {
                return NotFound();
            }
            _userManager.DeleteAsync(taskResult.Result);
            return new NoContentResult();
        }
    }
}
