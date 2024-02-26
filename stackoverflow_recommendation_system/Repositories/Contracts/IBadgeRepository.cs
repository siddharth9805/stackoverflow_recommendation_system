using stackoverflow_recommendation_system.Models;

namespace stackoverflow_recommendation_system.Repositories.Contracts
{
    public interface IBadgeRepository
    {
        public Task<IEnumerable<Badge>> GetBadgesByUserIds(IEnumerable<int> userId);
    }
}
