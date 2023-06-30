using System.ComponentModel.DataAnnotations;

namespace CookbookWebApp.Models
{
    public class UpdatePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
