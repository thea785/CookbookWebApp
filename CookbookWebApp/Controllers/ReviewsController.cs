using CookbookBLL;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApp.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult DeleteReview(int reviewid, int recipeid)
        {
            RecipeBLL.DeleteReview(reviewid);
            return RedirectToAction("GetRecipe", "Recipes", new { id = recipeid });
        }
    }
}
