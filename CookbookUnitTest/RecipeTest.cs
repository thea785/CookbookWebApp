using CookbookBLL;
using CookbookCommon;
using CookbookData;
using System.Xml.Linq;

namespace CookbookUnitTest
{
    [TestClass]
    public class RecipeTest
    {
        [TestMethod]
        public void TestCreateRecipe()
        {
            // Create a recipe and add it to the database
            Recipe r1 = new Recipe() { Name="testRecipe1", Servings=1, PrepTime=10, CookTime=20, Directions="testDirections1"};
            int id = RecipesData.CreateRecipe(r1);

            // Check if the recipe is in the database
            Assert.IsNotNull(RecipesData.GetRecipeByID(id));

            // Delete the recipe from the database
            RecipesData.DeleteRecipe(id);

            // Check that the recipe was deleted
            Assert.IsNull(RecipesData.GetRecipeByID(id));
        }

        [TestMethod]
        public void TestCreateRecipeWithIngredients()
        {
            // Create a recipe with ingredients and add it to the database
            Recipe r2 = new Recipe() { Name = "testRecipe2", Servings = 1, PrepTime = 10, CookTime = 20, Directions = "testDirections2" };
            Ingredient i1 = new Ingredient() { Name = "testIngredient1", Amount = 2, Units = "cups" };
            Ingredient i2 = new Ingredient() { Name = "testIngredient2", Amount = 1, Units = "tsp" };
            r2.Ingredients = new List<Ingredient> { i1, i2 };
            int r2_id = RecipeBLL.CreateRecipe(r2);

            // Check if the recipe is in the database
            Assert.IsNotNull(RecipeBLL.GetRecipeByID(r2_id));

            // Check if the recipe has the associated ingredients 
            Assert.IsTrue(RecipeBLL.GetIngredients(r2_id).Any(i => i.Name == "testIngredient1"));
            Assert.IsTrue(RecipeBLL.GetIngredients(r2_id).Any(i => i.Name == "testIngredient2"));

            // Clean up the ingredients and recipes
            RecipeBLL.DeleteRecipe(r2_id);

            // Check that the recipe was deleted
            Assert.IsNull(RecipeBLL.GetRecipeByID(r2_id));
            Assert.IsTrue(RecipeBLL.GetIngredients(r2_id).Count == 0);
        }

        [TestMethod]
        public void TestCreateRecipeWithReviews()
        {
            // Create a recipe with ingredients and add it to the database
            Recipe r2 = new Recipe() { Name = "testRecipe2", Servings = 1, PrepTime = 10, CookTime = 20, Directions = "testDirections2" };
            int r2_id = RecipeBLL.CreateRecipe(r2);

            // Check if the recipe is in the database
            Assert.IsNotNull(RecipeBLL.GetRecipeByID(r2_id));

            // Add reviews
            Review review1 = new Review() { RecipeID = r2_id, UserID = 1, ReviewText = "Example Text One" };
            Review review2 = new Review() { RecipeID = r2_id, UserID = 1, ReviewText = "Example Text Two" };
            RecipeBLL.CreateReview(review1);
            RecipeBLL.CreateReview(review2);

            // Check if the recipe has the associated reviews 
            Assert.IsTrue(RecipeBLL.GetReviewsByRecipeID(r2_id).Any(rev => rev.ReviewText == "Example Text One"));
            Assert.IsTrue(RecipeBLL.GetReviewsByRecipeID(r2_id).Any(rev => rev.ReviewText == "Example Text Two"));

            // Clean up the reviews and recipes
            RecipeBLL.DeleteReviewsByRecipeID(r2_id);
            RecipeBLL.DeleteRecipe(r2_id);

            // Check that the recipe was deleted
            Assert.IsNull(RecipeBLL.GetRecipeByID(r2_id));
            Assert.IsTrue(RecipeBLL.GetReviewsByRecipeID(r2_id).Count == 0);
        }

        [TestMethod]
        public void TestGetRecipes()
        {
            // Create two recipes and add them to the database
            Recipe r3 = new Recipe() { Name = "testRecipe3", Servings = 1, PrepTime = 10, CookTime = 20, Directions = "testDirections3" };
            Recipe r4 = new Recipe() { Name = "testRecipe4", Servings = 1, PrepTime = 10, CookTime = 20, Directions = "testDirections4" };
            int r3_id = RecipesData.CreateRecipe(r3);
            int r4_id = RecipesData.CreateRecipe(r4);

            // Check if the recipes are in the database
            List<Recipe> recipes = RecipesData.GetRecipes();
            Assert.IsNotNull(recipes);
            Assert.IsTrue(recipes.Any(r => r.Name == "testRecipe3"));
            Assert.IsTrue(recipes.Any(r => r.Name == "testRecipe4"));

            // Delete the recipe from the database
            RecipesData.DeleteRecipe(r3_id);
            RecipesData.DeleteRecipe(r4_id);
        }
    }
}