using api.Models;
using api.Mappings;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace api.Data
{
    public class DataContext
    {
        private readonly string _connectionString;
        private readonly IEnumerable<IEntityMap> _entityMaps;

        public DataContext(string connectionString, IEnumerable<IEntityMap> entityMaps)
        {
            _connectionString = connectionString;
            _entityMaps = entityMaps;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public void OnModelCreating()
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();

                foreach (var entityMap in _entityMaps)
                {
                    entityMap.Configure();
                }
            }
        }
    }
}
