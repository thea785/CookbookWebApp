using System.ComponentModel.DataAnnotations;

namespace CookbookWebApp.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Servings { get; set; }
        [Required]
        public int PrepTime { get; set; }
        [Required]
        public int CookTime { get; set; }
        [Required]
        public string? Directions { get; set; }
        public List<IngredientModel>? Ingredients { get; set; }
    }
}
