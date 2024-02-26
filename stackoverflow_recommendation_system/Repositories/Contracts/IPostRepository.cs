using stackoverflow_recommendation_system.Models;

namespace stackoverflow_recommendation_system.Repositories.Contracts
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsByBodyContent(string searchKey, int offset);
    }
}
