using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookCommon
{
    public class Recipe
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Servings { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public string? Directions { get; set; }
    }
}
