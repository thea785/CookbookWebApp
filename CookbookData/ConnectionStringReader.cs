using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookData
{
    internal class ConnectionStringReader
    {
        public static string Get()
        {
            return "Data Source=DESKTOP-GPLJ87I;Initial Catalog=Cookbook;Integrated Security=True";
        }
    }
}
