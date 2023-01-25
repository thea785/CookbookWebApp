using CookbookCommon;
using CookbookData;

namespace CookbookBLL
{
    public static class RecipeBLL
    {
        public static List<Recipe> GetRecipes()
        {
            return null;
        }
        public static Recipe GetRecipeById(int id)
        {
            return null;
        }
        public static int CreateRecipe(Recipe r) {
            return RecipesData.CreateRecipe(r);
        }
        public static bool DeleteRecipe(Recipe r) {
            return false; 
        }
        // TODO: Add SearchRecipe and EditRecipe
    }
}