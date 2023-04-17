using System;


namespace CookbookCommon
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string? Name { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public string? Directions { get; set; }
        public List<Ingredient>? Ingredients { get; set;}
        public List<Review>? Reviews { get; set; }
    }
}
