﻿using System;

namespace CookbookCommon
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public string? ReviewText { get; set; }
    }
}
