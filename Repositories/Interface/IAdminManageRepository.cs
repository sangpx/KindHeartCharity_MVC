using KindHeartCharity.Models.Domain;

namespace KindHeartCharity.Repositories.Interface
{
    public interface IAdminManageRepository
    {
        Task<List<Post>> GetAllAsync();

        Task<Post?> GetByIdAsync(Guid id);

        Task<Post> CreateAsync(Post post);

        //Task CreateAsync(Post post, IFormFile image);

        bool UpdateAsync(Post post);

        Task<Post> DeleteAsync(Guid id);
    }
}
