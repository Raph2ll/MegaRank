using api.Data.Repositories;
using api.Models;
using System.Collections.Generic;

namespace api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUser();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public User GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }
        public void UpdateUser(User updatedUser)
        {
            _userRepository.Update(updatedUser);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
        }
    }
}
