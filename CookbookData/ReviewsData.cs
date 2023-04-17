using CookbookCommon;
using System.Data;
using System.Data.SqlClient;

namespace CookbookData
{
    public static class ReviewsData
    {
        // Add the given recipe to the table and returns its RecipeID
        public static int CreateReview(Review r)
        {
            string connString = ConnectionStringReader.Get();

            try
            {
                int _pk = -1;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateReview", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramRecipeID = _sqlCommand.CreateParameter();
                        _paramRecipeID.DbType = DbType.Int32;
                        _paramRecipeID.ParameterName = "@RecipeID";
                        _paramRecipeID.Value = r.RecipeID;
                        _sqlCommand.Parameters.Add(_paramRecipeID);

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@UserID";
                        _paramUserID.Value = r.UserID;
                        _sqlCommand.Parameters.Add(_paramUserID);

                        SqlParameter _paramReviewText = _sqlCommand.CreateParameter();
                        _paramReviewText.DbType = DbType.String;
                        _paramReviewText.ParameterName = "@ReviewText";
                        _paramReviewText.Value = r.ReviewText;
                        _sqlCommand.Parameters.Add(_paramReviewText);

                        SqlParameter _paramReviewIDReturn = _sqlCommand.CreateParameter();
                        _paramReviewIDReturn.DbType = DbType.Int32;
                        _paramReviewIDReturn.ParameterName = "@OutReviewID";
                        var pk = _sqlCommand.Parameters.Add(_paramReviewIDReturn);
                        _paramReviewIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramReviewIDReturn.Value;
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

        // Returns a list of reviews for a given recipe
        public static List<Review> GetReviewsByRecipeID(int recipeID)
        {
            string connString = ConnectionStringReader.Get();

            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetReviewsByRecipeID", dbcon))
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
                            // Read in rows from SqlDataReader
                            while (reader.Read())
                            {
                                Review _review  = new Review()
                                {
                                    ReviewID = reader["ReviewID"] is DBNull ? 0 : (int)reader["ReviewID"],
                                    RecipeID = reader["RecipeID"] is DBNull ? 0 : (int)reader["RecipeID"],
                                    UserID = reader["UserID"] is DBNull ? 0 : (int)reader["UserID"],
                                    ReviewText = reader["ReviewText"] is DBNull ? "" : (string)reader["Name"]
                                };

                                reviews.Add(_review);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return reviews;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return reviews;
            }
        }

        // Returns a list of reviews for a given user
        public static List<Review> GetReviewByUserID(int userID)
        {
            string connString = ConnectionStringReader.Get();

            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetReviewsByUserID", dbcon))
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
                            // Read in Customer records from SqlDataReader
                            while (reader.Read())
                            {
                                Review _review = new Review()
                                {
                                    ReviewID = reader["ReviewID"] is DBNull ? 0 : (int)reader["ReviewID"],
                                    RecipeID = reader["RecipeID"] is DBNull ? 0 : (int)reader["RecipeID"],
                                    UserID = reader["UserID"] is DBNull ? 0 : (int)reader["UserID"],
                                    ReviewText = reader["ReviewText"] is DBNull ? "" : (string)reader["Name"]
                                };

                                reviews.Add(_review);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return reviews;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return reviews;
            }
        }

        // Deletes the given review
        public static void DeleteReview(int id)
        {
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("DeleteReview", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramReviewID = _sqlCommand.CreateParameter();
                        _paramReviewID.DbType = DbType.Int32;
                        _paramReviewID.ParameterName = "@ReviewID";
                        _paramReviewID.Value = id;
                        _sqlCommand.Parameters.Add(_paramReviewID);

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