using System;
using api.Models;

namespace api.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        List<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByName(string name);
        void UpdateUser(User updatedUser);
        void DeleteUser(int userId);
    }
}