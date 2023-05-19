namespace CookbookWebApp.Models
{
    public class ReviewModel
    {
        public int ReviewID { get; set; }
        public int RecipeID { get; set; }
        public string? RecipeName { get; set; }
        public string? UserEmail { get; set; }
        public string? ReviewText { get; set; }
    }
}