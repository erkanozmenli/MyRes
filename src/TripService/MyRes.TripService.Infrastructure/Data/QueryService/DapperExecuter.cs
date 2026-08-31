using System.Data;
using System.Data.Common;
using Dapper;

namespace MyRes.TripService.Infrastructure.Data.QueryService
{
    internal interface IDapperExecuter
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<IDictionary<string, object>>> QueryRawAsync(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    }

    internal class DapperExecutor : IDapperExecuter
    {
        private readonly IDbConnection _connection;

        public DapperExecutor(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await ((DbConnection)_connection).OpenAsync(cancellationToken);
            }

            var result = await _connection.QueryAsync<T>(
                    sql,
                    param,
                    commandType: commandType
                );

            return result.AsList();
        }

        /// <summary>
        /// Testing/Debugging purpose
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<IDictionary<string, object>>> QueryRawAsync(string sql, object? param = null, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            if (_connection.State != ConnectionState.Open)
            {
                await ((DbConnection)_connection).OpenAsync(cancellationToken);
            }

            var result = await _connection.QueryAsync(
                    sql,
                    param,
                    commandType: commandType
                );

            return result.Cast<IDictionary<string, object>>().ToList();
        }
    }
}
