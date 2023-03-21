using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CookbookWebApp.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Home")]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Field must be atleast 8 characters")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{6,}$", ErrorMessage = "At least one uppercase letter, 1 number and 1 special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}