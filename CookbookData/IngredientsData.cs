using CookbookCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookData
{
    public static class IngredientsData
    {
        const string connString =
            "Data Source=DESKTOP-GPLJ87I;Initial Catalog=Cookbook;Integrated Security=True";

        public static int CreateIngredient(Ingredient ingredient)
        {
            try
            {
                int _pk = -1;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateIngredient", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.String;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = ingredient.RecipeID;
                        _sqlCommand.Parameters.Add(_paramRecipeID);

                        SqlParameter _paramName = _sqlCommand.CreateParameter();
                        _paramName.DbType = DbType.String;
                        _paramName.ParameterName = "@Name";
                        _paramName.Value = ingredient.Name;
                        _sqlCommand.Parameters.Add(_paramName);

                        SqlParameter _paramAmount = _sqlCommand.CreateParameter();
                        _paramAmount.DbType = DbType.Int32;
                        _paramAmount.ParameterName = "@Amount";
                        _paramAmount.Value = ingredient.Amount;
                        _sqlCommand.Parameters.Add(_paramAmount);

                        SqlParameter _paramUnits = _sqlCommand.CreateParameter();
                        _paramUnits.DbType = DbType.String;
                        _paramUnits.ParameterName = "@Units";
                        _paramUnits.Value = ingredient.Units;
                        _sqlCommand.Parameters.Add(_paramUnits);

                        SqlParameter _paramIngredientIDReturn = _sqlCommand.CreateParameter();
                        _paramIngredientIDReturn.DbType = DbType.Int32;
                        _paramIngredientIDReturn.ParameterName = "@OutIngredientID";
                        var pk = _sqlCommand.Parameters.Add(_paramIngredientIDReturn);
                        _paramIngredientIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramIngredientIDReturn.Value;
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

        // Delete all ingredients for a given recipe
        public static void DeleteIngredients(int recipeID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("DeleteIngredients", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = recipeID;
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

        // Returns a list of ingredients for a given recipe
        public static List<Ingredient> GetIngredients(int recipeID)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetIngredients", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramRecipeID = cmd.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = recipeID;
                        cmd.Parameters.Add(_paramRecipeID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read in Customer records from SqlDataReader
                            while (reader.Read())
                            {
                                Ingredient _ingredient = new Ingredient()
                                {
                                    IngredientID = reader["IngredientID"] is DBNull ? 0 : (int)reader["IngredientID"],
                                    RecipeID = reader["RecipeID"] is DBNull ? 0 : (int)reader["RecipeID"],
                                    Name = reader["Name"] is DBNull ? "" : (string)reader["Name"],
                                    Amount = reader["Amount"] is DBNull ? -1 : (int)reader["Amount"],
                                    Units = reader["Units"] is DBNull ? "" : (string)reader["Units"]
                                };

                                ingredients.Add(_ingredient);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return ingredients;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return ingredients;
            }
        }
    }
}
