using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using FrequentPatternMining.Entities;


namespace FrequentPatternMining.DAL.SqlServer
{
    /// <summary>
    /// Provide functionality for retrieving data from Sql Server
    /// </summary>
    public sealed class SqlHelper
    {
        private SqlHelper() { }

        private static SqlCommand prepareCommand(String connectionString, String spName, SqlParameter[] colParams)
        {
            if (connectionString == null || connectionString.Length == 0)
                connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;

            SqlConnection cn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand(spName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            if (colParams != null)
            {
                foreach (SqlParameter objParam in colParams)
                    cmd.Parameters.Add(objParam);
            }

            return (cmd);
        }

        /// <summary>
        /// Prepare the execution of a stored procedure 
        /// </summary>
        /// <returns>Il SqlCommand configurato</returns>
        public static SqlCommand PrepareCommand()
        {
            return prepareCommand(null, String.Empty, null);
        }

        /// <summary>
        /// Prepare the execution of a stored procedure 
        /// </summary>
        /// <param name="sqlConnectionString">Database connectionString</param>
        /// <returns>a SqlCommand</returns>
        public static SqlCommand PrepareCommand(string sqlConnectionString)
        {
            return prepareCommand(sqlConnectionString, String.Empty, null);
        }

        /// <summary>
        /// Execute a stored procedure with the specified input parameters
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="colParams">Stored procedure parameter</param>
        /// <returns>Il SqlDataReader del resultset emesso dalla sp</returns>
        public static SqlDataReader ExecSPForReader(String spName, SqlParameter[] colParams)
        {
            return ExecSPForReader(null, spName, colParams);
        }

        /// <summary>
        /// Execute a SqlCommand 
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>A SqlDataReader resultset</returns>
        public static SqlDataReader ExecSPForReader(SqlCommand command)
        {
            command.CommandTimeout = 30;

            SqlDataReader drResult = null;
            try
            {
                command.Connection.Open();
                drResult = command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException exDB)
            {
                throw exDB;
            }
            return drResult;
        }

        /// <summary>
        /// Execute a stored procedure
        /// </summary>
        /// <param name="connectionString">Connection string used by sqlCommand</param>
        /// <param name="spName">Stored Procedure name</param>
        /// <param name="colParams">Stored Procedure parameters</param>
        /// <returns>A SqlDataReader containing resultset</returns>
        public static SqlDataReader ExecSPForReader(String connectionString, String spName, SqlParameter[] colParams)
        {
            SqlCommand cmd = prepareCommand(connectionString, spName, colParams);

            return ExecSPForReader(cmd);
        }
    }
}
