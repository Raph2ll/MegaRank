using System.Data;
using MySql.Data.MySqlClient;

namespace api.Mappings
{
    public class RolesMap : IEntityMap
    {
        private readonly string connectionString;

        public RolesMap(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Configure()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Role (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(128) NOT NULL
                )";

                using (var command = new MySqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
