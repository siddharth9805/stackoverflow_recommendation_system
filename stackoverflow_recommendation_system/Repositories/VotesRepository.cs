using Dapper;
using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Models.Context;
using stackoverflow_recommendation_system.Repositories.Contracts;

namespace stackoverflow_recommendation_system.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly DbContext _dbContext;

        public VotesRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<Vote>> GetVotesCount(IEnumerable<int> postIds)
        {
            var query = "SELECT PostId, COUNT(*) AS VotesCount FROM Votes WHERE PostId IN @postIds GROUP BY PostId";
            using var connection = _dbContext.CreateConnection();
            var votes = await connection.QueryAsync<Vote>(query, new { postIds });
            return votes;
        }
    }
}
