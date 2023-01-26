using CookbookCommon;
using CookbookData;

namespace CookbookUnitTest
{
    [TestClass]
    public class RecipeTest
    {
        [TestMethod]
        public void TestCreateRecipe()
        {
            Recipe r1 = new Recipe() { Name="Turkey Sandwich", Servings=1, PrepTime=5, CookTime=0, Directions="1. Toast Bread Slices 2. Apply Mayo 3. Assemble sandwich"};

            int id = RecipesData.CreateRecipe(r1);

            Assert.AreEqual(RecipesData.GetRecipeByID(id).RecipeID, id);

            RecipesData.DeleteRecipe(id);

            Assert.IsNull(RecipesData.GetRecipeByID(id));
        }
    }
}