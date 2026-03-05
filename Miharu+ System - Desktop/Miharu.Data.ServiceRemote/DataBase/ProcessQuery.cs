using Miharu.Data.ServiceRemote.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Miharu.Data.ServiceRemote.DataBase
{
    public class ProcessQuery
    {
        public string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MiharuConnectionString"].ConnectionString;
        }

        public QueryResponse execute(QueryRequest queryRequest)
        {
            if (queryRequest.parameters == null)
                queryRequest.parameters = new List<QueryParameter>();

            QueryResponse queryResponse = new QueryResponse();
            try
            {
                if (queryRequest.queryRequestType == QueryRequestType.StoredProcedure)
                {
                    queryResponse = executeStoredProcedure(queryRequest);
                }
                else
                {
                    queryResponse.dataTable = executeTableQuery(queryRequest);
                    queryResponse.rows = queryResponse.dataTable.Rows.Count;
                    queryResponse.success = true;
                }
            }
            catch (Exception ex)
            {
                queryResponse.error = ex.Message;
                queryResponse.success = false;
            }
            return queryResponse;
        }

        private QueryResponse executeStoredProcedure(QueryRequest queryRequest)
        {
            QueryResponse queryResponse = new QueryResponse();
            switch (queryRequest.queryResponseType)
            {
                case QueryResponseType.Table:
                    queryResponse.dataTable = executeDataTable(queryRequest);
                    queryResponse.rows = queryResponse.dataTable.Rows.Count;
                    queryResponse.success = true;
                    break;
                case QueryResponseType.Scalar:
                    queryResponse.scalar = executeScalar(queryRequest);
                    queryResponse.success = true;
                    break;
                case QueryResponseType.NonQuery:
                    queryResponse.rows = executeNonQuery(queryRequest);
                    queryResponse.success = true;
                    break;
                default:
                    queryResponse.error = "Tipo operacion desconocida";
                    queryResponse.success = false;
                    break;
            }
            return queryResponse;
        }

        private DataTable executeDataTable(QueryRequest queryRequest)
        {
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(getConnectionString());
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = queryRequest.name;
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    ////object valueToAssign = Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue : parameter.value;
                    //object valueToAssign = (Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue :
                    //                                                                              (DateTime.TryParse(parameter.value, out DateTime dateValue) ? (object)dateValue : parameter.value));
                    //sqlCommand.Parameters["@" + parameter.name].Value = valueToAssign;

                    string paramName = "@" + parameter.name;
                    var sqlParam = sqlCommand.Parameters[paramName];

                    AssignParameterValueFromRawString(sqlParam, parameter.value);
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                dataTable.TableName = "Response";
                return dataTable;
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
        }


        private Object executeScalar(QueryRequest queryRequest)
        {
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(getConnectionString());
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = queryRequest.name;
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    ////object valueToAssign = Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue : parameter.value;
                    //object valueToAssign = (Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue :
                    //                                                          (DateTime.TryParse(parameter.value, out DateTime dateValue) ? (object)dateValue : parameter.value));

                    //sqlCommand.Parameters["@" + parameter.name].Value = valueToAssign;
                    //

                    string paramName = "@" + parameter.name;
                    var sqlParam = sqlCommand.Parameters[paramName];

                    AssignParameterValueFromRawString(sqlParam, parameter.value);

                    //string paramName = "@" + parameter.name;
                    //string rawValue = parameter.value;

                    //object valueToAssign = rawValue;

                    //var sqlParam = sqlCommand.Parameters[paramName];

                    //// Analizar tipo de SQL Server
                    //switch (sqlParam.SqlDbType)
                    //{
                    //    case SqlDbType.UniqueIdentifier:
                    //        if (Guid.TryParse(rawValue, out Guid guidVal))
                    //            valueToAssign = guidVal;
                    //        else
                    //            valueToAssign = DBNull.Value;
                    //        break;

                    //    case SqlDbType.DateTime:
                    //    case SqlDbType.Date:
                    //        if (DateTime.TryParse(rawValue, out DateTime dateVal))
                    //            valueToAssign = dateVal;
                    //        else
                    //            valueToAssign = DBNull.Value;
                    //        break;

                    //    case SqlDbType.VarBinary:
                    //        try
                    //        {
                    //            // Convertir Base64 a byte[]
                    //            valueToAssign = Convert.FromBase64String(rawValue);
                    //        }
                    //        catch
                    //        {
                    //            valueToAssign = DBNull.Value;
                    //        }
                    //        break;

                    //    default:
                    //        valueToAssign = rawValue;
                    //        break;
                    //}

                    //sqlParam.Value = valueToAssign ?? DBNull.Value;
                }

                return sqlCommand.ExecuteScalar();
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
        }

        private int executeNonQuery(QueryRequest queryRequest)
        {
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(getConnectionString());
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = queryRequest.name;
                SqlCommandBuilder.DeriveParameters(sqlCommand);
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    ////object valueToAssign = Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue : parameter.value;
                    //object valueToAssign = (Guid.TryParse(parameter.value, out Guid guidValue) ? (object)guidValue :
                    //                                                          (DateTime.TryParse(parameter.value, out DateTime dateValue) ? (object)dateValue : parameter.value));
                    //sqlCommand.Parameters["@" + parameter.name].Value = valueToAssign;

                    string paramName = "@" + parameter.name;
                    var sqlParam = sqlCommand.Parameters[paramName];

                    AssignParameterValueFromRawString(sqlParam, parameter.value);
                }
                return sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
        }

        private DataTable executeTableQuery(QueryRequest queryRequest)
        {
            SqlConnection sqlConnection = null;

            string sqlstring = "select * from " + queryRequest.name;

            if (queryRequest.parameters.Count > 0)
            {
                sqlstring += " where ";
                int i = 0;
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    if (i > 0)
                    {
                        sqlstring += " and ";
                    }
                    sqlstring += "[" + parameter.name + "]=@" + parameter.name;
                    i++;
                }
            }

            try
            {
                sqlConnection = new SqlConnection(getConnectionString());
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlstring;
                foreach (QueryParameter parameter in queryRequest.parameters)
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@" + parameter.name, parameter.value));
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                sqlDataAdapter.Dispose();
                dataTable.TableName = "Response";
                return dataTable;
            }
            finally
            {
                try
                {
                    sqlConnection.Close();
                }
                catch { }
            }
        }

        private void AssignParameterValueFromRawString(SqlParameter sqlParam, string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                sqlParam.Value = DBNull.Value;
                return;
            }

            try
            {
                switch (sqlParam.SqlDbType)
                {
                    case SqlDbType.UniqueIdentifier:
                        sqlParam.Value = Guid.TryParse(rawValue, out Guid guidVal) ? (object)guidVal : DBNull.Value;
                        break;

                    case SqlDbType.DateTime:
                    case SqlDbType.Date:
                        sqlParam.Value = DateTime.TryParse(rawValue, out DateTime dateVal) ? (object)dateVal : DBNull.Value;
                        break;

                    case SqlDbType.VarBinary:
                        sqlParam.Value = Convert.FromBase64String(rawValue);
                        break;

                    default:
                        sqlParam.Value = rawValue;
                        break;
                }
            }
            catch
            {
                sqlParam.Value = DBNull.Value;
            }
        }
    }
}