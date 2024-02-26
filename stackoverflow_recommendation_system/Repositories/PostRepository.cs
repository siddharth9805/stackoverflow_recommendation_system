using Dapper;
using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Models.Context;
using stackoverflow_recommendation_system.Repositories.Contracts;

namespace stackoverflow_recommendation_system.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbContext _dbContext;

        public PostRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<IEnumerable<Post>> GetPostsByBodyContent(string searchKey, int offset)
        {
            var query = "WITH _questions AS (" +
                    "SELECT Title," +
                        "Body, AnswerCount, OwnerUserId" +
                    "FROM Posts2" +
                    "INNER JOIN PostTypes" +
                    "ON Posts2.PostTypeId = PostTypes.Id" +
                    "WHERE PostTypes.Type = 'Question'" +
                    "AND CONTAINS(Posts2.Body, @searchKey)" +
                "), _answers AS (" +
                    "SELECT questions.Title," +
                        "questions.Body," +
                        "questions.AnswerCount," +
                        "questions.OwnerUserId" +
                    "FROM Posts2 answers" +
                    "INNER JOIN PostTypes" +
                        "ON answers.PostTypeId = PostTypes.Id" +
                    "INNER JOIN Posts2 questions" +
                        "ON answers.ParentId = questions.Id" +
                    "WHERE PostTypes.Type = 'Answer'" +
                        "AND CONTAINS(answers.Body, @searchKey)" +
                "), _combined AS (" +
                    "SELECT * FROM _questions" +
                    "UNION" +
                    "SELECT * FROM _answers" +
                    "ORDER BY AnswerCount DESC" +
                    "OFFSET @offset ROWS" +
                    "FETCH NEXT 1000 ROWS ONLY" +
                ")" +
                "SELECT * FROM _combined";
            using var connection = _dbContext.CreateConnection();
            var posts = connection.QueryAsync<Post>(query, new { searchKey, offset });
            return posts;
        }
    }
}
