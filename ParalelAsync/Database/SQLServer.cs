using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ParalelAsync.Database
{
    internal class SQLServer
    {
        private readonly string _connectionString;

        public SQLServer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public T Scalar<T>(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();
                        return (T)sqlCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<T> ScalarAsync<T>(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        await sqlConnection.OpenAsync();
                        return (T)await sqlCommand.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public T Reader<T>(string query, SqlParameter[] parameters = null) where T : class, new()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        sqlConnection.Open();

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                T result = new T();
                                int count = reader.FieldCount-1;
                                for(int i = 0; i <= count; i++)
                                {
                                    string columna = reader.GetName(i);
                                    PropertyInfo propiedad = result.GetType().GetProperty(columna);

                                    if(propiedad != null && !reader.IsDBNull(i))
                                    {
                                        propiedad.SetValue(result, reader.GetValue(i));
                                    }
                                }
                                return result;
                            }
                            return null;
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> ReaderAsync<T>(string query, SqlParameter[] parameters = null) where T : class, new()
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            sqlCommand.Parameters.AddRange(parameters);
                        }

                        await sqlConnection.OpenAsync();

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                T result = new T();

                                string[] columnas = reader.GetSchemaTable().Rows
                                    .Cast<DataRow>().Select(row => row["ColumnName"].ToString()).ToArray();

                                foreach(string column in columnas)
                                {
                                    PropertyInfo propiedad = result.GetType().GetProperty(column);
                                    if(propiedad != null && !reader.IsDBNull(reader.GetOrdinal(column)))
                                    {
                                        object value = reader.GetValue(reader.GetOrdinal(column));
                                        propiedad.SetValue(result, value);
                                    }
                                }
                                return result;
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
