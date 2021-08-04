using System;

namespace Back.Zone.Storage.MySQL.Configuration
{
    public sealed class MysqlConfiguration
    {
        public readonly string Server;
        public readonly int Port;
        public readonly string Database;
        public readonly string Username;
        public readonly string Password;
        public readonly string SslMode;

        public MysqlConfiguration(MysqlConfigurationReader mysqlConfigurationReader)
        {
            var (server, port, database, username, password, sslMode) = mysqlConfigurationReader;

            if (server != null &&
                port != null &&
                database != null &&
                username != null &&
                password != null &&
                sslMode != null)
            {
                Server = server;
                Port = port.Value;
                Database = database;
                Username = username;
                Password = password;
                SslMode = sslMode;
            }
            else
            {
                throw new ArgumentException("Check Mysql section on the config file!");
            }
        }
    }

    public sealed record MysqlConfigurationReader(
        string? Server,
        int? Port,
        string? Database,
        string? Username,
        string? Password,
        string? SslMode
    )
    {
        public const string SectionIndicator = "Mysql";

        public MysqlConfigurationReader() : this(default, default, default, default, default, default)
        {
        }
    }
}