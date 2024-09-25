﻿using System.ComponentModel.DataAnnotations;

namespace Mvc03.Demo.PL.ViewModels.Auth
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Is Required !!")]
        [EmailAddress(ErrorMessage = "Email Is Required !!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
