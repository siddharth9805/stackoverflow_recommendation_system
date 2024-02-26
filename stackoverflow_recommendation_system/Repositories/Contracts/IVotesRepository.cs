using stackoverflow_recommendation_system.Models;

namespace stackoverflow_recommendation_system.Repositories.Contracts
{
    public interface IVotesRepository
    {
        public Task<IEnumerable<Vote>> GetVotesCount(IEnumerable<int> postIds);
    }
}
