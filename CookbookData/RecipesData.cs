using CookbookCommon;
using System.Data;
using System.Data.SqlClient;

namespace CookbookData
{
    public static class RecipesData
    {
        const string connString =
            "Data Source=DESKTOP-GPLJ87I;Initial Catalog=Cookbook;Integrated Security=True";

        // Add the given recipe to the table and returns its RecipeID
        public static int CreateRecipe(Recipe r)
        {
            try
            {
                int _pk = -1;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateRecipe", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramName = _sqlCommand.CreateParameter();
                        _paramName.DbType = DbType.String;
                        _paramName.ParameterName = "@Name";
                        _paramName.Value = r.Name;
                        _sqlCommand.Parameters.Add(_paramName);

                        SqlParameter _paramServings = _sqlCommand.CreateParameter();
                        _paramServings.DbType = DbType.Int32;
                        _paramServings.ParameterName = "@Servings";
                        _paramServings.Value = r.Servings;
                        _sqlCommand.Parameters.Add(_paramServings);

                        SqlParameter _paramPrepTime = _sqlCommand.CreateParameter();
                        _paramPrepTime.DbType = DbType.Int32;
                        _paramPrepTime.ParameterName = "@PrepTime";
                        _paramPrepTime.Value = r.PrepTime;
                        _sqlCommand.Parameters.Add(_paramPrepTime);

                        SqlParameter _paramCookTime = _sqlCommand.CreateParameter();
                        _paramCookTime.DbType = DbType.Int32;
                        _paramCookTime.ParameterName = "@CookTime";
                        _paramCookTime.Value = r.CookTime;
                        _sqlCommand.Parameters.Add(_paramCookTime);

                        SqlParameter _paramDirections = _sqlCommand.CreateParameter();
                        _paramDirections.DbType = DbType.String;
                        _paramDirections.ParameterName = "@Directions";
                        _paramDirections.Value = r.Directions;
                        _sqlCommand.Parameters.Add(_paramDirections);

                        SqlParameter _paramRecipeIDReturn = _sqlCommand.CreateParameter();
                        _paramRecipeIDReturn.DbType = DbType.Int32;
                        _paramRecipeIDReturn.ParameterName = "@OutRecipeID";
                        var pk = _sqlCommand.Parameters.Add(_paramRecipeIDReturn);
                        _paramRecipeIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramRecipeIDReturn.Value;
                        con.Close();
                        return _pk;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return -1;
            }
        }
    }
}