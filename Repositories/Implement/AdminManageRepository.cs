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


        public async Task<bool> UpdateAsync(Post post)
        {
            authDbContext.posts.Update(post);
            await authDbContext.SaveChangesAsync();
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


        public async Task<List<Post>> SearchByName(string name)
        {
            return await authDbContext.posts.Where(p => EF.Functions.Like(p.Content, $"%{name}%")).ToListAsync();
        }

        public async Task<object> UpdatePost(Guid postId, string content, string description, DateTime postDate, IFormFile imageFile)
        {
            var post = await authDbContext.posts.FindAsync(postId);
            if (post == null)
            {
                return new
                {
                    success = false,
                    message = "Product not found"
                };
            }
            post.PostDate = postDate;
            post.Content = content;
            post.Description = description;
            post.PostId = postId;

            if (imageFile != null)
            {
                var imagePath = Path.Combine(environment.WebRootPath, "Uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(imagePath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                post.PostImageURL = fileName;
            }

            await authDbContext.SaveChangesAsync();

            return new
            {
                success = true,
                message = "Update Successfully!",
                post
            };


        }
    }
}
