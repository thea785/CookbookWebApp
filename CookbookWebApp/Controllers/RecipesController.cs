using CookbookBLL;
using CookbookCommon;
using CookbookWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CookbookWebApp.Controllers
{
    public class RecipesController : Controller
    {
        // Returns index view of recipes
        public IActionResult Index()
        {
            // Get Recipes and convert them to RecipeModel objects
            List<Recipe> recipes = RecipeBLL.GetRecipes();
            List<RecipeModel> recipeModels = new List<RecipeModel>();

            // Check for null value
            if (recipes == null) { return View(); }

            foreach(Recipe r in recipes)
            {
                recipeModels.Add(Mapper.RecipeToRecipeModel(r));
            }

            return View(recipeModels);
        }

        // Redirect to Create view
        public IActionResult Create()
        {
            return View();
        }

        // View details of a given recipe
        public IActionResult Recipe(int recipeID)
        {
            return View();
        }

        // Delete a given recipe
        public IActionResult Delete(int recipeID)
        {
            return View();
        }
    }
}
