using System;
using api.Models;

namespace api.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        List<User> GetAllUsers();
        User GetUserById(int id);
        void UpdateUser(User updatedUser);
        void DeleteUser(int userId);
    }
}