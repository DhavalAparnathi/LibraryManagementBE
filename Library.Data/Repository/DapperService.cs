using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Library.Data.Repository
{
    public class DapperService : IDapperService
    {
        private readonly string _connString;
        private readonly int _defaultTimeout = 3000;

        public DapperService(string connectionString)
        {
            _connString = connectionString;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connString);

        // Execute a command that returns a scalar (single value)
        // Returns the first column of the first row.
        public object ExecuteScalar(string storedProcedure, object? parameters = null) 
        {
            using var connection = CreateConnection();
            return connection.ExecuteScalar(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        // Execute a command that returns affected rows count
        // Returns int - number of affected rows
        public int Execute(string storedProcedure, object? parameters = null)
        {
            using var connection = CreateConnection();
            return connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        // Query a list of entities. Method maps results to list of T
        // IEnumerable<T>
        public IEnumerable<T> Query<T>(string storedProcedure, object? parameters = null) where T : class
        {
            using var connection = CreateConnection();
            return connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        // Query single entity. Maps the first result (or null) to type T
        // Returns T?
        public T? QueryFirstOrDefault<T>(string storedProcedure, object? parameters = null) where T : class
        {
            using var connection = CreateConnection();
            return connection.QueryFirstOrDefault<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        // Query that returns a scalar value of type T
        // Returns a single scalar value of type T
        public T ExecuteScalar<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using var connection = CreateConnection();
            return connection.ExecuteScalar<T>(sql, parameters, commandType: commandType);
        }

        // Query that returns multiple result sets and maps them to different types.
        // Returns (List<TFirst>, TSecond) tuple.
        public (List<TFirst> FirstResult, TSecond SecondResult) QueryMultiple<TFirst, TSecond>(string storedProcedure, object? parameters = null) where TFirst : class
        {
            using var connection = CreateConnection();
            connection.Open();
            using var multi = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

            var firstResult = multi.Read<TFirst>().ToList();
            var secondResult = multi.Read<TSecond>().FirstOrDefault();
            return (firstResult, secondResult);
        }
    }
}