using CookbookCommon;
using System.Data.SqlClient;
using System.Data;

namespace CookbookData
{
    public static class UsersData
    {
        public static int CreateUser(int roleID, string email, string firstName, string lastName, string hashedPassword, string salt)
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                int _pk = 0;
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("CreateUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _RoleID = _sqlCommand.CreateParameter();
                        _RoleID.DbType = DbType.Int32;
                        _RoleID.ParameterName = "@RoleID";
                        _RoleID.Value = roleID;
                        _sqlCommand.Parameters.Add(_RoleID);

                        SqlParameter _Email = _sqlCommand.CreateParameter();
                        _Email.DbType = DbType.String;
                        _Email.ParameterName = "@Email";
                        _Email.Value = email;
                        _sqlCommand.Parameters.Add(_Email);

                        SqlParameter _FirstName = _sqlCommand.CreateParameter();
                        _FirstName.DbType = DbType.String;
                        _FirstName.ParameterName = "@FirstName";
                        _FirstName.Value = firstName;
                        _sqlCommand.Parameters.Add(_FirstName);

                        SqlParameter _LastName = _sqlCommand.CreateParameter();
                        _LastName.DbType = DbType.String;
                        _LastName.ParameterName = "@LastName";
                        _LastName.Value = lastName;
                        _sqlCommand.Parameters.Add(_LastName);

                        SqlParameter _HashedPassword = _sqlCommand.CreateParameter();
                        _HashedPassword.DbType = DbType.String;
                        _HashedPassword.ParameterName = "@HashedPassword";
                        _HashedPassword.Value = hashedPassword;
                        _sqlCommand.Parameters.Add(_HashedPassword);

                        SqlParameter _Salt = _sqlCommand.CreateParameter();
                        _Salt.DbType = DbType.String;
                        _Salt.ParameterName = "@Salt";
                        _Salt.Value = salt;
                        _sqlCommand.Parameters.Add(_Salt);

                        SqlParameter _paramUserIDReturn = _sqlCommand.CreateParameter();
                        _paramUserIDReturn.DbType = DbType.Int32;
                        _paramUserIDReturn.ParameterName = "@paramOutUserID";
                        var pk = _sqlCommand.Parameters.Add(_paramUserIDReturn);
                        _paramUserIDReturn.Direction = ParameterDirection.Output;

                        con.Open();
                        _sqlCommand.ExecuteNonQuery(); // calls the sp
                        _pk = (int)_paramUserIDReturn.Value;
                        con.Close();
                        return _pk;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return 0;
            }
        }

        public static void DeleteUser(int userID)
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("DeleteUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _paramUserID = _sqlCommand.CreateParameter();
                        _paramUserID.DbType = DbType.Int32;
                        _paramUserID.ParameterName = "@paramUserID";
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

        public static void UpdateUserPassword(string email, string newHashedPassword, string newSalt)
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("UpdateUserPassword", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _Email = _sqlCommand.CreateParameter();
                        _Email.DbType = DbType.String;
                        _Email.ParameterName = "@Email";
                        _Email.Value = email;
                        _sqlCommand.Parameters.Add(_Email);

                        SqlParameter _NewHashedPassword = _sqlCommand.CreateParameter();
                        _NewHashedPassword.DbType = DbType.String;
                        _NewHashedPassword.ParameterName = "@NewHashedPassword";
                        _NewHashedPassword.Value = newHashedPassword;
                        _sqlCommand.Parameters.Add(_NewHashedPassword);

                        SqlParameter _NewSalt = _sqlCommand.CreateParameter();
                        _NewSalt.DbType = DbType.String;
                        _NewSalt.ParameterName = "@NewSalt";
                        _NewSalt.Value = newSalt;
                        _sqlCommand.Parameters.Add(_NewSalt);

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

        public static List<User> GetAllUsers()
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                List<User> users = new List<User>(); // Build a list of users to return

                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllUsers", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read in Customer records from SqlDataReader
                            while (reader.Read())
                            {
                                User _user = new User()
                                {
                                    UserID = (int)reader["UserID"],
                                    RoleID = (int)reader["RoleID"],
                                    Email = (string)reader["Email"],
                                    FirstName = reader["FirstName"] is DBNull ? "" : (string)reader["FirstName"],
                                    LastName = reader["LastName"] is DBNull ? "" : (string)reader["LastName"],
                                    HashedPassword = reader["HashedPassword"] is DBNull ? "" : (string)reader["HashedPassword"],
                                    Salt = reader["Salt"] is DBNull ? "" : (string)reader["Salt"]
                                };

                                users.Add(_user);
                            }
                        }
                    }
                    dbcon.Close();
                }
                return users;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return null;
            }
        }

        public static User GetUserByEmail(string email)
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection dbcon = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserByEmail", dbcon))
                    {
                        dbcon.Open(); // Open SqlConnection

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter _paramEmail = cmd.CreateParameter();
                        _paramEmail.DbType = DbType.String;
                        _paramEmail.ParameterName = "@paramEmail";
                        _paramEmail.Value = email;
                        cmd.Parameters.Add(_paramEmail);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Read in Customer records from SqlDataReader
                            if (reader.Read())
                            {
                                User _user = new User()
                                {
                                    UserID = reader["UserID"] is DBNull ? 0 : (int)reader["UserID"],
                                    RoleID = reader["RoleID"] is DBNull ? 0 : (int)reader["RoleID"],
                                    Email = reader["Email"] is DBNull ? "" : (string)reader["Email"],
                                    FirstName = reader["FirstName"] is DBNull ? "" : (string)reader["FirstName"],
                                    LastName = reader["LastName"] is DBNull ? "" : (string)reader["LastName"],
                                    HashedPassword = reader["HashedPassword"] is DBNull ? "" : (string)reader["HashedPassword"],
                                    Salt = reader["Salt"] is DBNull ? "" : (string)reader["Salt"],

                                };
                                if (_user.UserID == 0)
                                {
                                    dbcon.Close();
                                    return null;
                                }
                                else
                                {
                                    dbcon.Close();
                                    return _user;
                                }
                            }
                        }
                    }
                    dbcon.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionLogData.CreateExceptionLog(ex);
                return null;
            }
        }

        public static void EditUser(int userID, int roleID, string email, string firstName, string lastName)
        {
            //string connString = System.IO.File.ReadAllText("../CookbookData/ConnectionString");
            string connString = ConnectionStringReader.Get();

            try
            {
                using (SqlConnection con = new SqlConnection(connString))
                {
                    using (SqlCommand _sqlCommand = new SqlCommand("EditUser", con))
                    {
                        _sqlCommand.CommandType = CommandType.StoredProcedure;
                        _sqlCommand.CommandTimeout = 30;

                        SqlParameter _userID = _sqlCommand.CreateParameter();
                        _userID.DbType = DbType.Int32;
                        _userID.ParameterName = "@UserID";
                        _userID.Value = userID;
                        _sqlCommand.Parameters.Add(_userID);

                        SqlParameter _roleID = _sqlCommand.CreateParameter();
                        _roleID.DbType = DbType.Int32;
                        _roleID.ParameterName = "@RoleID";
                        _roleID.Value = roleID;
                        _sqlCommand.Parameters.Add(_roleID);

                        SqlParameter _Email = _sqlCommand.CreateParameter();
                        _Email.DbType = DbType.String;
                        _Email.ParameterName = "@Email";
                        _Email.Value = email;
                        _sqlCommand.Parameters.Add(_Email);

                        SqlParameter _firstName = _sqlCommand.CreateParameter();
                        _firstName.DbType = DbType.String;
                        _firstName.ParameterName = "@FirstName";
                        _firstName.Value = firstName;
                        _sqlCommand.Parameters.Add(_firstName);

                        SqlParameter _lastName = _sqlCommand.CreateParameter();
                        _lastName.DbType = DbType.String;
                        _lastName.ParameterName = "@LastName";
                        _lastName.Value = lastName;
                        _sqlCommand.Parameters.Add(_lastName);

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
    }
}

