using api.Models;

namespace api.Data.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        List<User> GetAllUser();
        User GetUserById(int id);
        void Update(User updatedUser);
        void Delete(int userId);
    }
}