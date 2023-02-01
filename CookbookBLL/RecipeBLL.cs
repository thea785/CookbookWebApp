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
        public static Recipe GetRecipeByID(int id)
        {
            return RecipesData.GetRecipeByID(id);
        }
        public static int CreateRecipe(Recipe r) {
            return RecipesData.CreateRecipe(r);
        }
        public static void DeleteRecipe(int id) {
             RecipesData.DeleteRecipe(id); 
        }
        public static bool UpdateRecipe(Recipe r) { 
            return false; 
        }
        // TODO: Add SearchRecipe
    }
}