using MySql.Data.MySqlClient;
using System.Data;

namespace api.Mappings
{
    public class RolesMap : IEntityMap
    {
        private bool RoleExists(MySqlConnection connection, string roleName)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT COUNT(*) FROM Role WHERE Name = @roleName";
                cmd.Parameters.AddWithValue("@roleName", roleName);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
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

                if (!RoleExists(connection, "user"))
                {
                    cmd.CommandText = @"
                        INSERT INTO Role (Name) VALUES ('user')";
                    cmd.ExecuteNonQuery();
                }

                if (!RoleExists(connection, "admin"))
                {
                    cmd.CommandText = @"
                        INSERT INTO Role (Name) VALUES ('admin')";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
