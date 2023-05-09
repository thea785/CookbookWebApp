using CookbookBLL;
using CookbookCommon;
using CookbookWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult CreateReview(ReviewModel rm)
        {
            Review r = new Review() { RecipeID = rm.RecipeID, ReviewText = rm.ReviewText };
            RecipeBLL.CreateReview(r);
            return RedirectToAction("GetRecipe", "Recipes", new { id = rm.RecipeID });
        }
        public IActionResult DeleteReview(int reviewid, int recipeid)
        {
            RecipeBLL.DeleteReview(reviewid);
            return RedirectToAction("GetRecipe", "Recipes", new { id = recipeid });
        }
    }
}
