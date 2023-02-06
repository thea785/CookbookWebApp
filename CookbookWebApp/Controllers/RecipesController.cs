using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApp.Controllers
{
    public class RecipesController : Controller
    {
        // Returns index view of recipes
        public IActionResult Index()
        {
            return View();
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
