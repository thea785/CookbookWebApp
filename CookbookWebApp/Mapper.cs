using CookbookCommon;
using CookbookWebApp.Models;

namespace CookbookWebApp
{
    public static class Mapper
    {
        public static RecipeModel RecipeToRecipeModel(Recipe r)
        {
            return new RecipeModel()
            {
                RecipeID= r.RecipeID,
                Name=r.Name,
                Servings=r.Servings,
                PrepTime=r.PrepTime,
                CookTime=r.CookTime,
                Directions=r.Directions
            }
        }
    }
}
