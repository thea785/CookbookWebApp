using System.ComponentModel.DataAnnotations;

namespace CookbookWebApp.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int Servings { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int PrepTime { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid number")]
        public int CookTime { get; set; }
        [Required]
        public string? Directions { get; set; }
        public List<IngredientModel>? Ingredients { get; set; }
        public string? IngredientsInput { get; set; }
        public List<ReviewModel>? Reviews { get; set; }
        public List<int>? Favorites { get; set; }
    }
}
