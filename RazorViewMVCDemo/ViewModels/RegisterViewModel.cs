using System;
using System.ComponentModel.DataAnnotations;

namespace RazorViewMVCDemo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Must be between 3 and 15")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Must be between 3 and 15")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be between 5 and 50")]
        public string Street { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be between 5 and 50")]
        public string State { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be between 5 and 50")]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
