using Dapper;
using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Models.Context;
using stackoverflow_recommendation_system.Repositories.Contracts;

namespace stackoverflow_recommendation_system.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = "SELECT * FROM PostTypes";
            using var connection = _dbContext.CreateConnection();
            var users = await connection.QueryAsync<User>(query);
            return users;
        }

        public async Task<IEnumerable<User>> GetUsers(IEnumerable<int> Ids)
        {
            var query = "SELECT * FROM Users WHERE Id IN @Ids";
            using var connection = _dbContext.CreateConnection();
            var users = await connection.QueryAsync<User>(query, new { Ids });
            return users;
        }

        public async Task<User> GetUserById(int Id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using var connection = _dbContext.CreateConnection();
            var users = await connection.QueryAsync<User>(query, new { Id });
            return users.ToList()[0];
        }

    }
}
