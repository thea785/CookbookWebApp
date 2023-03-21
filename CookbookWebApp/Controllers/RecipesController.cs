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
            // Set session for guest
            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                HttpContext.Session.SetInt32("UserID", -1);
                HttpContext.Session.SetInt32("RoleID", 1);
                HttpContext.Session.SetString("Email", "");
            }

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
        [HttpGet]
        public IActionResult CreateRecipe()
        {
            return View();
        }

        // Submit create recipe form
        [HttpPost]
        public IActionResult CreateRecipe(RecipeModel m)
        {
            if (!ModelState.IsValid)
                return View();

            return RedirectToAction("Index");
        }

        // View details of a given recipe
        public IActionResult GetRecipe(int id)
        {
            RecipeModel recipeModel = Mapper.RecipeToRecipeModel(RecipeBLL.GetRecipeByID(id));

            return View(recipeModel);
        }

        // Delete a given recipe
        public IActionResult Delete(int recipeID)
        {
            return View();
        }
    }
}
