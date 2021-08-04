using System;
using System.Threading.Tasks;
using Back.Zone.Monads.EitherMonad;
using MySql.Data.MySqlClient;

namespace Back.Zone.Storage.MySQL.Service.Interfaces
{
    public interface IMysqlService
    {
        public Either<Exception, TA> GetConnection<TA>(Func<MySqlConnection, TA> f);
        public Task<Either<Exception, TA>> GetConnectionAsync<TA>(Func<MySqlConnection, Task<TA>> f);
    }
}