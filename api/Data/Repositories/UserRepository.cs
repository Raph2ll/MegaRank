using System;
using System.Collections.Generic;
using System.Data;
using api.Models;
using MySql.Data.MySqlClient;


namespace api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _connection;

        public UserRepository(DataContext connection)
        {
            _connection = connection;
        }

        public void AddUser(User user)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var cmd = new MySqlCommand("INSERT INTO User (Name, Email,Slug,RolesId,PasswordHash, ) VALUES (@Name, @Email, @Slug, @RolesId, @PasswordHash)",
                    dbConnection))
                {
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.Password);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<User> GetAllUser()
        {
            var users = new List<User>();

            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand($"SELECT Id, Name, Email FROM MegaRank.User",
                    dbConnection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;

        }
        public User GetUserById(int id)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand("SELECT Name, Email,RoleId,Password FROM MegaRank.User WHERE Id = @Id",
                    dbConnection))
                {
                    command.Parameters.AddWithValue("@Id", id);


                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Password = Convert.ToString(reader["Password"])
                            };
                        }
                    }
                }
            }

            return null;

        }
        public User GetUserByName(string name)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand("SELECT Name, Email, RoleId, Password FROM MegaRank.User WHERE Name = @Name",
                    dbConnection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"]),
                                RoleId = Convert.ToInt32(reader["RoleId"]),
                                Password = Convert.ToString(reader["Password"])
                            };
                        }
                    }
                }
            }

            return null;

        }
        public List<User> GetByOrderDesc(int quantity)
        {
            var users = new List<User>();

            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand(
                    @"SELECT Id, Name, Email
                    FROM (
                        SELECT *
                        FROM MegaRank.User
                        ORDER BY Id DESC
                        LIMIT @Quantity
                    ) AS subquery
                    ORDER BY Id ASC;", dbConnection))
                {
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        public void Update(User updatedUser)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand("UPDATE MegaRank.User SET Name = @Name, Email = @Email WHERE Id = @Id",
                    dbConnection))
                {
                    command.Parameters.AddWithValue("@Id", updatedUser.Id);
                    command.Parameters.AddWithValue("@Name", updatedUser.Name);
                    command.Parameters.AddWithValue("@Email", updatedUser.Email);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int userId)
        {
            using (var dbConnection = _connection.GetConnection())
            {
                dbConnection.Open();
                using (var command = new MySqlCommand("DELETE FROM MegaRank.User WHERE Id = @userId",
                    dbConnection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
