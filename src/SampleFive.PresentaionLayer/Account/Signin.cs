using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleFive.PresentaionLayer.Account
{
    public class Signin
    {
        [Display(Name="نام کاربری")]
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Display(Name="کلمه عبور")]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}