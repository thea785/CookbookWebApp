using CookbookCommon;
using System.Data.SqlClient;
using System.Data;

namespace CookbookData
{
    internal class FavoritesData
    {
        // Creates a favorite for the given recipe and user
        public static void CreateFavorite(int recipeID, int userID)
        {
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateFavorite", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = recipeID;
                        _sqlCommand.Parameters.Add(_paramRecipeID);

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        _sqlCommand.Parameters.Add(_paramUserID);

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }
        // Returns a list of favorites for a given recipe
        public static List<int> GetFavoritesByRecipeID(int recipeID)
        {
            string connString = ConnectionStringReader.Get();

            List<int> favs = new List<int>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFavoritesByRecipeID", dbcon))
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
                            while (reader.Read())
                            {
                                favs.Add((int)reader["UserID"]);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return favs;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return favs;
            }
        }
        // Returns a list of favorites for a given user
        public static List<int> GetFavoritesByUserID(int userID)
        {
            string connString = ConnectionStringReader.Get();

            List<int> favs = new List<int>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetFavoritesByUserID", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramUserID = cmd.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        cmd.Parameters.Add(_paramUserID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                favs.Add((int)reader["RecipeID"]);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return favs;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return favs;
            }
        }
        // Deletes a specific Favorite
        public static void DeleteFavorite(int recipeID, int userID)
        {
            string connString = ConnectionStringReader.Get();

            List<int> favs = new List<int>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteFavorite", dbcon))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramRecipeID = cmd.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = recipeID;
                        cmd.Parameters.Add(_paramRecipeID);

                        SqlParameter _paramUserID = cmd.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        cmd.Parameters.Add(_paramUserID);

                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }
        // Deletes all Favorites for a recipe
        public static void DeleteFavoritesByRecipeID(int recipeID)
        {
            string connString = ConnectionStringReader.Get();

            List<int> favs = new List<int>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteFavoritesByRecipeID", dbcon))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramRecipeID = cmd.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = recipeID;
                        cmd.Parameters.Add(_paramRecipeID);

                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
            }
        }
        // Deletes Favorites for a user
        public static void DeleteFavoritesByUserID(int userID)
        {
            string connString = ConnectionStringReader.Get();

            List<int> favs = new List<int>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteFavorite", dbcon))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramUserID = cmd.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = userID;
                        cmd.Parameters.Add(_paramUserID);

                        dbcon.Open();
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
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
