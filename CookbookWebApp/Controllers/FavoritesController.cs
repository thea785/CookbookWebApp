using CookbookBLL;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult DeleteFavorite(int recipeID, int userID)
        {
            RecipeBLL.DeleteFavorite(recipeID, userID);
            return RedirectToAction("GetRecipe", "Recipes", new { id = recipeID });
        }

        public IActionResult CreateFavorite(int recipeID, int userID)
        {
            RecipeBLL.CreateFavorite(recipeID, userID);
            return RedirectToAction("GetRecipe", "Recipes", new { id = recipeID });
        }
    }
}
