using CookbookCommon;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CookbookData
{
    public static class RecipesData
    {
        //const string connString =
        //    "Data Source=DESKTOP-GPLJ87I;Initial Catalog=Cookbook;Integrated Security=True";

        // Add the given recipe to the table and returns its RecipeID
        public static int CreateRecipe(Recipe r)
        {
            string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");

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

        public static List<Recipe> GetRecipes()
        {
            string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");

            try
            {
                List<Recipe> recipes = new List<Recipe>();
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRecipes", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read in records from SqlDataReader
                            while (reader.Read())
                            {
                                Recipe r = new Recipe()
                                {
                                    RecipeID = reader["RecipeID"] is DBNull ? 0 : (int)reader["RecipeID"],
                                    Name = reader["Name"] is DBNull ? "" : (string)reader["Name"],
                                    Servings = reader["Servings"] is DBNull ? 0 : (int)reader["Servings"],
                                    PrepTime = reader["PrepTime"] is DBNull ? 0 : (int)reader["PrepTime"],
                                    CookTime = reader["CookTime"] is DBNull ? 0 : (int)reader["CookTime"],
                                    Directions = reader["Directions"] is DBNull ? "" : (string)reader["Directions"]
                                };

                                recipes.Add(r);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return recipes;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return null;
            }
        }

        public static Recipe GetRecipeByID(int id)
        {
            string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("GetRecipeByID", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = id;
                        _sqlCommand.Parameters.Add(_paramRecipeID);

                        con.Open();

                        using (SqlDataReader reader = _sqlCommand.ExecuteReader())
                        {
                            // Read in records from SqlDataReader
                            if (reader.Read())
                            {
                                Recipe r = new Recipe()
                                {
                                    RecipeID = reader["RecipeID"] is DBNull ? 0 : (int)reader["RecipeID"],
                                    Name = reader["Name"] is DBNull ? "" : (string)reader["Name"],
                                    Servings = reader["Servings"] is DBNull ? 0 : (int)reader["Servings"],
                                    PrepTime = reader["PrepTime"] is DBNull ? 0 : (int)reader["PrepTime"],
                                    CookTime = reader["CookTime"] is DBNull ? 0 : (int)reader["CookTime"],
                                    Directions = reader["Directions"] is DBNull ? "" : (string)reader["Directions"]
                                };
                                if (r.RecipeID == 0)
                                {
                                    con.Close();
                                    return null;
                                }
                                else
                                {
                                    con.Close();
                                    return r;
                                }
                            }
                        }

                        con.Close();
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return null;
            }
        }

        public static int UpdateRecipe(Recipe r)
        {
            string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateRecipe", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        var pk = _sqlCommand.Parameters.Add(_paramRecipeID);
                        _paramRecipeID.Direction = ParameterDirection.Output;

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

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return 0;
            }
            return 1;
        }

        public static void DeleteRecipe(int id)
        {
            string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("DeleteRecipe", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = id;
                        _sqlCommand.Parameters.Add(_paramRecipeID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery();

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }
    }
}