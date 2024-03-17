using System;
using System.Collections.Generic;
using System.Data;
using api.Models;
using MySql.Data.MySqlClient;


namespace api.Data.Repositories
{
    public class UserRepository
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
                    cmd.Parameters.AddWithValue("@Slug", user.Slug);
                    cmd.Parameters.AddWithValue("@RolesId", user.RolesId);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

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
                using (var command = new MySqlCommand("SELECT Name, Email,Slug,RolesId,PasswordHash FROM MegaRank.User WHERE Id = @Id",
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
                                Slug = Convert.ToString(reader["Slug"]),
                                RolesId = Convert.ToInt32(reader["RolesId"]),
                                PasswordHash = Convert.ToString(reader["PasswordHash"])
                            };
                        }
                    }
                }
            }

            return null;

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
