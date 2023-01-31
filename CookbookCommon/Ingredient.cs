﻿namespace CookbookCommon
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public int RecipeID { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public string? Units { get; set; }
    }
}