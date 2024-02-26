using stackoverflow_recommendation_system.Models;

namespace stackoverflow_recommendation_system.Repositories.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsers();
        public Task<IEnumerable<User>> GetUsers(IEnumerable<int> ids);
        public Task<User> GetUserById(int id);
    }
}
