using stackoverflow_recommendation_system.Models;

namespace stackoverflow_recommendation_system.ViewModels
{
    public class SearchResult
    {
        public string PostTitle { get; set; }
        public string Description { get; set; } // Trimmed to 140 chars in the service layer
        public int TotalVotes { get; set; }
        public int TotalAnswers { get; set; }
        public string UserName { get; set; }
        public int UserReputation { get; set; }
        public List<Badge>? Badges { get; set; }
        // Pagination properties
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } = 10; // Default page size
    }
}
