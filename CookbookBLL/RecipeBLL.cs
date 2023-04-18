using CookbookCommon;
using CookbookData;

namespace CookbookBLL
{
    public static class RecipeBLL
    {
        public static List<Recipe> GetRecipes()
        {
            return RecipesData.GetRecipes();
        }
        public static Recipe GetRecipeByID(int id)
        {
            Recipe recipe = RecipesData.GetRecipeByID(id);
            if (recipe != null)
            {
                recipe.Ingredients = GetIngredients(id);
                recipe.Reviews = GetReviewsByRecipeID(id);
            }
            return recipe;
        }
        public static List<Ingredient> GetIngredients(int recipeID)
        {
            return IngredientsData.GetIngredients(recipeID);
        }

        public static List<Review> GetReviewsByRecipeID(int id)
        {
            return ReviewsData.GetReviewsByRecipeID(id);
        }

        public static int CreateRecipe(Recipe r) {
            // Create the recipe
            int recipeID = RecipesData.CreateRecipe(r);

            if (r.Ingredients == null)
                return recipeID;

            // Add its ingredients
            foreach(Ingredient ingredientItem in r.Ingredients)
            {
                ingredientItem.RecipeID = recipeID;
                IngredientsData.CreateIngredient(ingredientItem);
            }
            // Return the recipe's id
            return recipeID;
        }

        public static int CreateReview(Review rev) 
        {
            return ReviewsData.CreateReview(rev);
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
        
    }
}