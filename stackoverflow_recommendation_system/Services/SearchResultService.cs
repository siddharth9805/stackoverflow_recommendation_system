using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Repositories.Contracts;
using stackoverflow_recommendation_system.ViewModels;

namespace stackoverflow_recommendation_system.Services
{
    public class SearchResultService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IPostRepository _postRepository;
        private readonly IVotesRepository _votesRepository;

        private int BatchOffset { get; set; } = 0;
        private bool LastBatchRetrieved { get; set; }
        private IEnumerable<Post>? Posts { get; set; }

        public SearchResultService(
            IUserRepository userRepository,
            IBadgeRepository badgeRepository,
            IPostRepository postRepository,
            IVotesRepository votesRepository)
        {
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            _postRepository = postRepository;
            _votesRepository = votesRepository;
        }

        public async Task<IEnumerable<SearchResult>?> GetSearchResults(string searchKey, int pageNumber)
        {
            var posts = await SearchPosts(searchKey, pageNumber);
            if (posts == null) { return null; }
            var userIds = posts.Select(post => post.OwnerUserId).ToList();
            var getUsers = _userRepository.GetUsers(userIds);
            var getBadges = _badgeRepository.GetBadgesByUserIds(userIds);
            var postIds = posts.Select(post => post.Id).ToList();
            var getVoteCounts = _votesRepository.GetVotesCount(postIds);

            await Task.WhenAll(getUsers, getBadges, getVoteCounts);

            var users = getUsers.Result.ToDictionary(user => user.Id);
            var badges = getBadges.Result.GroupBy(badge => badge.UserId).ToDictionary(group => group.Key, group => group.ToList());
            var voteCounts = getVoteCounts.Result.ToDictionary(voteCount => voteCount.PostId);

            var result = posts.Select(post => {
                var user = users.GetValueOrDefault(post.OwnerUserId, null);
                return new SearchResult
                {
                    PostTitle = post.Title,
                    Description = post.Body,
                    TotalVotes = voteCounts.GetValueOrDefault(post.Id)?.VotesCount ?? 0,
                    TotalAnswers = post.AnswerCount,
                    UserName = user?.DisplayName ?? String.Empty,
                    UserReputation = user?.Reputation ?? 0,
                    Badges = badges.GetValueOrDefault(post.OwnerUserId),
                    CurrentPage = pageNumber
                };
            });

            return result;
        }

        public async Task<IEnumerable<Post>?> SearchPosts(string searchKey, int pageNumber)
        {
            if (String.IsNullOrEmpty(searchKey))
            {
                return null;
            }
            searchKey = "\"" + searchKey + "\"";
            var retrievalOffset = pageNumber * 10;
            if (!LastBatchRetrieved && retrievalOffset >= BatchOffset - 10)
            {
                var posts = await GetNextBatchOfPosts(searchKey);
                if (Posts == null)
                {
                    Posts = posts;
                }
                else
                {
                    Posts = Posts.Concat(posts);
                }
            }

            return Posts?.Skip(retrievalOffset - 10).Take(10);
        }

        public async Task<IEnumerable<Post>> GetNextBatchOfPosts(string searchKey)
        {
            var posts = await _postRepository.GetPostsByBodyContent(searchKey, BatchOffset);
            if (posts.Count() < 1000)
            {
                LastBatchRetrieved = true;
            }
            BatchOffset += 1000;
            return posts;
        }
    }
}
