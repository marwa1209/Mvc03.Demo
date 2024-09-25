using System.ComponentModel.DataAnnotations;

namespace Mvc03.Demo.PL.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage = "Email Is Required !!")]
        public string Email { get; set; }
    }
}
