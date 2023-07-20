using KindHeartCharity.Data;
using KindHeartCharity.Models.Domain;
using KindHeartCharity.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace KindHeartCharity.Repositories.Implement
{
    public class AdminManageRepository : IAdminManageRepository
    {
        private readonly AuthDbContext authDbContext;
        private readonly IWebHostEnvironment environment;

        public AdminManageRepository(AuthDbContext authDbContext, IWebHostEnvironment environment)
        {
            this.authDbContext = authDbContext;
            this.environment = environment;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await authDbContext.posts.ToListAsync();
        }

        public Task<Post?> GetByIdAsync(Guid id)
        {
            return authDbContext.posts.FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await authDbContext.posts.AddAsync(post);
            await authDbContext.SaveChangesAsync();
            return post;
        }


        public bool UpdateAsync(Post post)
        {
            authDbContext.posts.Update(post);
            authDbContext.SaveChangesAsync();
            return true;
        }


        public async Task<Post?> DeleteAsync(Guid id)
        {
            var postExisting = authDbContext.posts.FirstOrDefault(p => p.PostId == id);
            if (postExisting == null) { return null; }
            authDbContext.posts.Remove(postExisting);
            await authDbContext.SaveChangesAsync();
            return postExisting;
        }


    }
}
