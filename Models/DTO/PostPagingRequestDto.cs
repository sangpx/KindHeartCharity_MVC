using KindHeartCharity.Models.Domain;

namespace KindHeartCharity.Models.DTO
{
    public class PostPagingRequestDto
    {
        public IQueryable<Post> PostList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? Term { get; set; }
    }
}
