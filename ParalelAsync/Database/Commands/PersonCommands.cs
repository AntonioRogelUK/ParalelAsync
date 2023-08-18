using ParalelAsync.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelAsync.Database.Commands
{
    internal class PersonCommands
    {
        private readonly string _connectionString;

        public PersonCommands(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Person GetPerson (int id)
        {
            try
            {
                if (id < 1) throw new ArgumentException();

                StringBuilder query = new StringBuilder();

                query.Append("SELECT ");
                query.Append("[BusinessEntityID]");
                query.Append(",[FirstName]");
                query.Append(",[MiddleName]");
                query.Append(",[LastName]");
                query.Append(",[ModifiedDate]");
                query.Append(" FROM [Person].[Person]");
                query.Append(" WHERE ");
                query.Append("[BusinessEntityID] = @BusinessEntityID");

                SqlParameter[] sqlParameters = 
                    {
                        new SqlParameter("@BusinessEntityID", id)
                    };

                SQLServer sql = new SQLServer(_connectionString);

                Person person = sql.Reader<Person>(query.ToString(), sqlParameters);

                return person == null ? throw new Exception("Persona no encontrada") : person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            try
            {
                if (id < 1) throw new ArgumentException();

                StringBuilder query = new StringBuilder();

                query.Append("SELECT ");
                query.Append("[BusinessEntityID]");
                query.Append(",[FirstName]");
                query.Append(",[MiddleName]");
                query.Append(",[LastName]");
                query.Append(",[ModifiedDate]");
                query.Append(" FROM [Person].[Person]");
                query.Append(" WHERE ");
                query.Append("[BusinessEntityID] = @BusinessEntityID");

                SqlParameter[] sqlParameters =
                    {
                        new SqlParameter("@BusinessEntityID", id)
                    };

                SQLServer sql = new SQLServer(_connectionString);

                Person person = await sql.ReaderAsync<Person>(query.ToString(), sqlParameters);

                return person == null ? throw new Exception("Persona no encontrada") : person;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
