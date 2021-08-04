using System;
using System.Threading.Tasks;
using Back.Zone.Monads.EitherMonad;
using Back.Zone.Storage.MySQL.Configuration;
using Back.Zone.Storage.MySQL.Service.Interfaces;
using MySql.Data.MySqlClient;

namespace Back.Zone.Storage.MySQL.Service
{
    public class MysqlService : IMysqlService
    {
        private readonly string _connectionString;

        private static string BuildConnectionString(MysqlConfiguration mysqlConfiguration) =>
            $"server={mysqlConfiguration.Server};port={mysqlConfiguration.Port.ToString()};user id={mysqlConfiguration.Username}; password={mysqlConfiguration.Password}; database={mysqlConfiguration.Database}; SslMode={mysqlConfiguration.SslMode}";

        private MySqlConnection AcquireConnection() => new(_connectionString);

        public MysqlService(MysqlConfiguration mysqlConfiguration)
        {
            _connectionString = BuildConnectionString(mysqlConfiguration);
        }

        public Either<Exception, TA> GetConnection<TA>(Func<MySqlConnection, TA> f)
        {
            var connection = AcquireConnection();

            try
            {
                connection.Open();
                return f(connection);
            }
            catch (Exception e)
            {
                return e;
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Either<Exception, TA>> GetConnectionAsync<TA>(Func<MySqlConnection, Task<TA>> f)
        {
            var connection = AcquireConnection();

            try
            {
                await connection.OpenAsync();
                return await f(connection);
            }
            catch (Exception e)
            {
                return e;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}