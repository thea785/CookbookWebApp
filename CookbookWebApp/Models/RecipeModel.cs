namespace CookbookWebApp.Models
{
    public class RecipeModel
    {
        public int RecipeID { get; set; }
        public string? Name { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public string? Directions { get; set; }
        public List<IngredientModel>? Ingredients { get; set; }
    }
}
