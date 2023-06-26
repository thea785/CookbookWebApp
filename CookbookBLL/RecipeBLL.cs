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
                recipe.Favorites = FavoritesData.GetFavoritesByRecipeID(id);
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

        public static void DeleteReview(int reviewID)
        {
            ReviewsData.DeleteReview(reviewID);
        }
        public static void DeleteReviewsByRecipeID(int recipeID)
        {
            ReviewsData.DeleteReviewsByRecipeID(recipeID);
        }

        public static void DeleteReviewsByUserID(string userEmail)
        {
            ReviewsData.DeleteReviewsByUserEmail(userEmail);
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

            // Delete the favorites
            FavoritesData.DeleteFavoritesByRecipeID(recipeID);

            // Delete the recipe's reviews
            ReviewsData.DeleteReviewsByRecipeID(recipeID);

            // Finally, delete the recipe
             RecipesData.DeleteRecipe(recipeID); 
        }

        public static bool UpdateRecipe(Recipe r) { 
            return false; 
        }
        
        public static void CreateFavorite(int recipeID, int userID) 
        {
            FavoritesData.CreateFavorite(recipeID, userID);
        }
        public static void DeleteFavorite(int recipeID, int userID)
        {
            FavoritesData.DeleteFavorite(recipeID, userID);
        }

        public static List<Favorite> GetFavorites(int userID)
        {
            return FavoritesData.GetFavoritesByUserID(userID);
        }

        // Given an ingredient input string, return a list of ingredient objects
        public static List<Ingredient> ParseIngredientInput(string input, int recipeID)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            // Split the ingredient string by comma
            string[] ingredientStrings = input.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
            
            foreach (string s in ingredientStrings)
            {
                // Split each ingredient by space character
                string[] splitString = s.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

                // Check if the splitString is valid
                int amount;
                if (splitString.Length >= 3 && Int32.TryParse(splitString[0], out amount) && splitString[1].Length > 0 && splitString[2].Length > 0)
                {
                    // Combine ingredient name tokens into one
                    string name = splitString[2];
                    for (int i = 3; i < splitString.Length; i++)
                    {
                        name += " " + splitString[i];
                    }

                    // Add the ingredient into the return list
                    ingredients.Add(new Ingredient()
                    {
                        RecipeID = recipeID,
                        Name = name,
                        Amount = amount,
                        Units = splitString[1]
                    });
                }
            }

            return ingredients;
        }
    }
}