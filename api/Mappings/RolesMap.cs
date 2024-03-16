using MySql.Data.MySqlClient;
using System.Data;

namespace api.Mappings
{
    public class RolesMap : IEntityMap
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
                    CREATE TABLE IF NOT EXISTS Role (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        Name VARCHAR(128) NOT NULL
                    );";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
