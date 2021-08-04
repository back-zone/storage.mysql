using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Back.Zone.Storage.MySQL.Models.Shared
{
    public sealed record QueryResult(
        string Message,
        bool Result,
        string? ObjectId
    )
    {
        public static QueryResult From(Exception exception) => new(exception.Message, true, null);

        public static void Set(MySqlCommand mySqlCommand)
        {
            mySqlCommand.Parameters.Add(new MySqlParameter("o_result", MySqlDbType.Int32));
            mySqlCommand.Parameters["o_result"].Direction = ParameterDirection.Output;

            mySqlCommand.Parameters.Add(new MySqlParameter("o_message", MySqlDbType.VarChar));
            mySqlCommand.Parameters["o_message"].Direction = ParameterDirection.Output;

            mySqlCommand.Parameters.Add(new MySqlParameter("o_id", MySqlDbType.VarChar));
            mySqlCommand.Parameters["o_id"].Direction = ParameterDirection.Output;
        }

        public static QueryResult Get(MySqlCommand mySqlCommand)
        {
            var result = (int)mySqlCommand.Parameters["o_result"].Value != 0;

            var message = mySqlCommand.Parameters["o_message"].Value switch
            {
                string s => s,
                _ => "#UNKWN#"
            };

            var objectId = mySqlCommand.Parameters["o_id"].Value switch
            {
                string s => s,
                _ => null
            };

            return new QueryResult(message, result, objectId);
        }
    }
}