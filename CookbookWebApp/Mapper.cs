using CookbookCommon;
using CookbookWebApp.Models;

namespace CookbookWebApp
{
    public static class Mapper
    {
        public static RecipeModel RecipeToRecipeModel(Recipe r)
        {
            // Create a recipe model object to return
            RecipeModel _rmodel =  new RecipeModel()
            {
                RecipeID = r.RecipeID,
                Name = r.Name,
                Servings = r.Servings,
                PrepTime = r.PrepTime,
                CookTime = r.CookTime,
                Directions = r.Directions,
                Ingredients = new List<IngredientModel>(),
                Reviews = new List<ReviewModel>()
            };

            // Check for null ingredients in parameter
            if (r.Ingredients == null)
            {
                return _rmodel;
            }

            // Copy ingredient values 
            foreach(Ingredient ingredient in r.Ingredients)
            {
                _rmodel.Ingredients.Add(new IngredientModel()
                {
                    IngredientID= ingredient.IngredientID,
                    RecipeID = ingredient.RecipeID,
                    Name = ingredient.Name,
                    Amount = ingredient.Amount,
                    Units= ingredient.Units
                });
            }

            if (r.Reviews == null)
                return _rmodel;

            // Copy Review values
            foreach (Review currentReview in r.Reviews)
            {
                _rmodel.Reviews.Add(new ReviewModel()
                {
                    ReviewID = currentReview.ReviewID,
                    RecipeID = currentReview.RecipeID,
                    UserEmail = currentReview.UserEmail,
                    ReviewText = currentReview.ReviewText
                });
            }

            // Return the recipe model
            return _rmodel;
        }

        public static Recipe ModelToRecipe(RecipeModel r)
        {
            // Create a recipe object to return
            Recipe _recipe = new Recipe()
            {
                RecipeID = r.RecipeID,
                Name = r.Name,
                Servings = r.Servings,
                PrepTime = r.PrepTime,
                CookTime = r.CookTime,
                Directions = r.Directions,
                Ingredients = new List<Ingredient>()
            };

            // Check for null ingredients in parameter
            if (r.Ingredients == null)
            {
                return _recipe;
            }

            // Copy ingredient values into 
            foreach (IngredientModel iModel in r.Ingredients)
            {
                _recipe.Ingredients.Add(new Ingredient()
                {
                    IngredientID = iModel.IngredientID,
                    RecipeID = iModel.RecipeID,
                    Name = iModel.Name,
                    Amount = iModel.Amount,
                    Units = iModel.Units
                });
            }

            // Return the recipe model
            return _recipe;
        }
    }
}
