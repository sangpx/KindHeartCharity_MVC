using KindHeartCharity.Models.Domain;

namespace KindHeartCharity.Repositories.Interface
{
     public interface IAdminManageRepository
     {
          Task<List<Post>> GetAllAsync();

          Task<Post?> GetByIdAsync(Guid id);

          Task<Post> CreateAsync(Post post);

          Task<Object> UpdateAsync(Guid PostId, string Content, string Description);

          Task<Post> DeleteAsync(Guid id);

          Task<List<Post>> SearchByName(string name);
     }
}
