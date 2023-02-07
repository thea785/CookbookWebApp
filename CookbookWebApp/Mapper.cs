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
                Ingredients = new List<IngredientModel>()
            };

            // Check for null ingredients in parameter
            if (r.Ingredients == null)
            {
                return _rmodel;
            }

            // Copy ingredient values into 
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

            // Return the recipe model
            return _rmodel;
        }
    }
}
