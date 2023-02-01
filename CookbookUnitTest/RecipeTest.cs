using CookbookBLL;
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

            int id = RecipeBLL.CreateRecipe(r1);

            Assert.IsNotNull(RecipeBLL.GetRecipeByID(id));

            RecipeBLL.DeleteRecipe(id);

            Assert.IsNull(RecipeBLL.GetRecipeByID(id));
        }

        [TestMethod]
        public void TestCreateRecipeWithIngredients()
        {

        }
    }
}