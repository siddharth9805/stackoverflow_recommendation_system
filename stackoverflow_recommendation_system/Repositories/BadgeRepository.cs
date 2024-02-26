using Dapper;
using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Models.Context;
using stackoverflow_recommendation_system.Repositories.Contracts;

namespace stackoverflow_recommendation_system.Repositories
{
    public class BadgeRepository : IBadgeRepository
    {
        private readonly DbContext _dbContext;

        public BadgeRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Badge>> GetBadgesByUserIds(IEnumerable<int> userIds)
        {
            var query = "SELECT * FROM Badges WHERE UserId = @userIds ORDER BY UserId";
            using var connection = _dbContext.CreateConnection();
            var badges = await connection.QueryAsync<Badge>(query, new {userIds});
            return badges;
        }
    }
}
