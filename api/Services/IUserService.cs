using System;
using api.Models;

namespace api.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        List<User> GetAllUser();
        User GetUserById(int id);
        void Update(User updatedUser);
        void Delete(int userId);
    }
}