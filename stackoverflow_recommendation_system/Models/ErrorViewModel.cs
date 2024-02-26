namespace stackoverflow_recommendation_system.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    
    public class search_prompt
    {
        public string searchQuery { get; set; }
    }

}
