using System;
using MySql.Data.MySqlClient;

namespace api.Mappings
{
    public interface IEntityMap
    {
        void Configure(MySqlConnection connection);
    }
}
