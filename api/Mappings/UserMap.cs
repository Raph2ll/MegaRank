using MySql.Data.MySqlClient;
using System.Data;

namespace api.Mappings
{
    public class UserMap : IEntityMap
    {
        public void Configure(MySqlConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                // connection.Open();

                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS MegaRank";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"USE MegaRank";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS User (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Name VARCHAR(80) NOT NULL,
                        Email VARCHAR(160) NOT NULL,
                        Slug VARCHAR(80) NOT NULL,
                        PasswordHash VARCHAR(255) NOT NULL,
                        RoleId INT NOT NULL,
                        FOREIGN KEY (RoleId) REFERENCES Roles(Id)
                        UNIQUE (Slug)
                    );";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
