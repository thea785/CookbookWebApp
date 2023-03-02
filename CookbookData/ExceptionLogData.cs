using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace CookbookData
{
    public static class ExceptionLogData
    {
        public static int CreateExceptionLog(Exception inException)
        {
            string connString = System.IO.File.ReadAllText("ConnectionString");

            int _pk = 0;
            using (SqlConnection con = new SqlConnection(connString))
            {

                using (SqlCommand _sqlCommand = new SqlCommand("CreateExceptionLog", con))
                {

                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandTimeout = 30;

                    SqlParameter _parmStackTrace = _sqlCommand.CreateParameter();
                    _parmStackTrace.DbType = DbType.String;
                    _parmStackTrace.ParameterName = "@StackTrace";
                    _parmStackTrace.Value = inException.StackTrace;
                    _sqlCommand.Parameters.Add(_parmStackTrace);

                    SqlParameter _parmMessage = _sqlCommand.CreateParameter();
                    _parmMessage.DbType = DbType.String;
                    _parmMessage.ParameterName = "@Message";
                    _parmMessage.Value = inException.Message;
                    _sqlCommand.Parameters.Add(_parmMessage);

                    SqlParameter _parmSource = _sqlCommand.CreateParameter();
                    _parmSource.DbType = DbType.String;
                    _parmSource.ParameterName = "@Source";
                    _parmSource.Value = inException.Source;
                    _sqlCommand.Parameters.Add(_parmSource);

                    SqlParameter _parmURL = _sqlCommand.CreateParameter();
                    _parmURL.DbType = DbType.String;
                    _parmURL.ParameterName = "@Url";
                    _parmURL.Value = DBNull.Value; // TODO maybe in include URL in web app
                    _sqlCommand.Parameters.Add(_parmURL);

                    SqlParameter _parmLogDate = _sqlCommand.CreateParameter();
                    _parmLogDate.DbType = DbType.DateTime;
                    _parmLogDate.ParameterName = "@LogDate";
                    _parmLogDate.Value = DateTime.Now;
                    _sqlCommand.Parameters.Add(_parmLogDate);

                    SqlParameter _paramExceptionLoggingIDReturn = _sqlCommand.CreateParameter();
                    _paramExceptionLoggingIDReturn.DbType = DbType.Int32;
                    _paramExceptionLoggingIDReturn.ParameterName = "@parmOutExceptionLoggingID";
                    var pk = _sqlCommand.Parameters.Add(_paramExceptionLoggingIDReturn);
                    _paramExceptionLoggingIDReturn.Direction = ParameterDirection.Output;

                    con.Open();
                    _sqlCommand.ExecuteNonQuery(); // calls the sp
                    _pk = (int)_paramExceptionLoggingIDReturn.Value; // has the auto incremented value returned
                    con.Close();
                    return _pk;

                }
            }
        }
    }
}