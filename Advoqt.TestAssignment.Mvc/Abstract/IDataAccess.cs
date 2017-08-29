namespace Advoqt.TestAssignment.Mvc.Abstract
{
    using System.Collections.Generic;

    /// <summary>
    /// Database access contract.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Gets a sequence of result-set rows.
        /// </summary>
        /// <typeparam name="T">Type of the result-set rows.</typeparam>
        /// <param name="sql">The SQL query.</param>
        /// <param name="parameters">An anonymous class instance containing the parameter names and values required by the query.</param>
        /// <returns>
        /// The sequence of result-set rows.
        /// </returns>
        IEnumerable<T> GetSequence<T>(string sql, object parameters = null);

        /// <summary>
        /// Executes the specified SQL query.
        /// </summary>
        /// <param name="sql">The SQL query.</param>
        /// <param name="parameters">An anonymous class instance containing the parameter names and values required by the query.</param>
        /// <returns>
        /// Whether the operation affected any rows.
        /// </returns>
        bool Execute(string sql, object parameters = null);
    }
}