using KindHeartCharity.Models.Domain;

namespace KindHeartCharity.Repositories.Interface
{
    public interface IAdminManageRepository
    {
        Task<List<Post>> GetAllAsync();

        Task<Post?> GetByIdAsync(Guid id);

        Task<Post> CreateAsync(Post post);

        Task<bool> UpdateAsync(Post post);

        Task<object> UpdatePost(Guid id, string Content, string Description, DateTime PostDate, IFormFile imageFile);

        Task<Post> DeleteAsync(Guid id);

        Task<List<Post>> SearchByName(string name);


    }
}
