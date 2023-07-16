using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.DTOs.RequestDTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(26, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,26}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one digit")]
        public string Password { get; set; }

        public string Error { get; set; }
    }

}
