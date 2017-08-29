namespace Advoqt.TestAssignment.Mvc.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using Abstract;
    using Dapper;

    /// <summary>
    /// Database access.
    /// </summary>
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccess"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Gets a sequence of result-set rows.
        /// </summary>
        /// <typeparam name="T">Type of the result-set rows.</typeparam>
        /// <param name="sql">The SQL query.</param>
        /// <param name="parameters">An anonymous class instance containing the parameter names and values required by the query.</param>
        /// <returns>
        /// The sequence of result-set rows.
        /// </returns>
        public IEnumerable<T> GetSequence<T>(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Executes the specified SQL query.
        /// </summary>
        /// <param name="sql">The SQL query.</param>
        /// <param name="parameters">An anonymous class instance containing the parameter names and values required by the query.</param>
        /// <returns>
        /// Whether the operation affected any rows.
        /// </returns>
        public bool Execute(string sql, object parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Execute(sql, parameters) > 0;
            }
        }
    }
}
