using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace RazorViewMVCDemo.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(15, MinimumLength =3, ErrorMessage ="Must be between 3 and 15")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Must be between 3 and 15")]
        public string FirstName { get; set; }

        public string Gender { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(50, MinimumLength =5, ErrorMessage = "Must be between 5 and 50")]
        public string Street { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be between 5 and 50")]
        public string State { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Must be between 5 and 50")]
        public string Country { get; set; }

        public IEnumerable<Photo> Photos { get; set; }

        public User()
        {
            Photos = new List<Photo>();
        }

    }
}
