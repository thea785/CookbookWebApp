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
        public static List<Ingredient> GetIngredients(int recipeID)
        {
            return IngredientsData.GetIngredients(recipeID);
        }
        public static int CreateRecipe(Recipe r) {
            // Create the recipe
            int recipeID = RecipesData.CreateRecipe(r);
            // Add its ingredients
            foreach(Ingredient ingredientItem in r.Ingredients)
            {
                System.Console.WriteLine(ingredientItem.Name);
                IngredientsData.CreateIngredient(ingredientItem);
            }
            // Return the recipe's id
            return recipeID;
        }
        public static void DeleteRecipe(int recipeID) {
            // First, delete the recipe's ingredients
            IngredientsData.DeleteIngredients(recipeID);
            
            // Then, delete the recipe
             RecipesData.DeleteRecipe(recipeID); 
        }
        public static bool UpdateRecipe(Recipe r) { 
            return false; 
        }
        // TODO: Add SearchRecipe
    }
}