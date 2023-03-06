using System.ComponentModel.DataAnnotations;

namespace CookbookWebApp.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        [Required]
        public string? Name { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        [Required]
        public string? Directions { get; set; }
        public List<IngredientModel>? Ingredients { get; set; }
    }
}
