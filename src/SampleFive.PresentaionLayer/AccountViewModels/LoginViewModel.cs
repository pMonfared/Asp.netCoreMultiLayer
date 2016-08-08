using System.ComponentModel.DataAnnotations;

namespace SampleFive.PresentaionLayer.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The {0} field is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        [Display(Name = "Sum")]
        public string Captcha { get; set; }
    }
}
