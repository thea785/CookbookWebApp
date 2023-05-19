using CookbookBLL;
using CookbookCommon;
using CookbookWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult CreateReview(int recipeid)
        {
            ReviewModel rm = new ReviewModel() { RecipeID = recipeid};
            return View(rm);
        }
        public IActionResult CreateReviewSubmit(ReviewModel rm)
        {
            Review r = new Review() { RecipeID = rm.RecipeID, ReviewText = rm.ReviewText, UserEmail=rm.UserEmail };
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
