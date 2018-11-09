using CodeGen.Utility;
using System.Data;
using System.Data.SqlClient;

namespace CodeGen
{
    public static class SqlHelper
    {
        #region Public Method

        public static IDataReader ExeuReader(string sql)
        {
            var connection = GetConnection();
            var command = new SqlCommand(sql, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        #endregion Public Method

        #region Private Method

        private static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigHelper.ConnectionString);
        }

        #endregion Private Method
    }
};