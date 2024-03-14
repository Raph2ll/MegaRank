using System.Data;
using MySql.Data.MySqlClient;

namespace api.Mappings
{
    public class UserMap : IEntityMap
    {
        private readonly string connectionString;

        public UserMap(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Configure()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS User (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(80) NOT NULL,
                    Email VARCHAR(160) NOT NULL,
                    PasswordHash VARCHAR(255) NOT NULL,
                    Slug VARCHAR(80) NOT NULL,
                    UNIQUE (Slug)
                )";

                using (var command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createIndexQuery = "CREATE UNIQUE INDEX IF NOT EXISTS IX_User_Slug ON User (Slug)";
                using (var command = new MySqlCommand(createIndexQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
