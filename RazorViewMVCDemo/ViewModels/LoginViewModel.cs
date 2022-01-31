using System;
using System.ComponentModel.DataAnnotations;

namespace RazorViewMVCDemo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required oooooooooOOOOOOO!!!!!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
