using CookbookBLL;
using Microsoft.AspNetCore.Mvc;
using CookbookWebApp.Models;
using CookbookCommon;

namespace CookbookWebApp.Controllers
{
    public class FavoritesController : Controller
    {
        public IActionResult GetFavorites(int userID)
        {
            List<Favorite> favorites = RecipeBLL.GetFavorites(userID);

            List<FavoriteModel> favoriteModels = new List<FavoriteModel>();

            foreach (Favorite favorite in favorites)
            {
                favoriteModels.Add(new FavoriteModel()
                {
                    RecipeID = favorite.RecipeID,
                    RecipeName = favorite.RecipeName
                });
            }
            return View(favoriteModels);
        }
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
