using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    public interface IDapperService
    {
        object ExecuteScalar(string storedProcedure, object? parameters = null);
        int Execute(string storedProcedure, object? parameters = null);
        IEnumerable<T> Query<T>(string storedProcedure, object? parameters = null) where T : class;
        T? QueryFirstOrDefault<T>(string storedProcedure, object? parameters = null) where T : class;
        T ExecuteScalar<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text);
        (List<TFirst> FirstResult, TSecond SecondResult) QueryMultiple<TFirst, TSecond>(string storedProcedure, object? parameters = null) where TFirst : class;
    }
}
