using KindHeartCharity.Data;
using KindHeartCharity.Models;
using KindHeartCharity.Models.Domain;
using KindHeartCharity.Models.DTO;
using KindHeartCharity.Repositories.Implement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KindHeartCharity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthDbContext authDbContext;

        public HomeController(ILogger<HomeController> logger, AuthDbContext authDbContext)
        {
            _logger = logger;
            this.authDbContext = authDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Post> posts = await authDbContext.posts.ToListAsync();
            //var paginatedList = PaginatedList<Post>.Create(posts, 1, 3);
            //return View(paginatedList);
            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            if (!ModelState.IsValid) { return View(); }

            var post = await authDbContext.posts.FirstOrDefaultAsync(p => p.PostId == id);
            return View(post);
        }

        public async Task<IActionResult> Paging(int page = 1, int pageSize = 3)
        {
            var posts = await authDbContext.posts.ToListAsync();
            var paginatedList = PaginatedList<Post>.Create(posts, page, pageSize);
            return PartialView("PostPagingResults", paginatedList);
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var postLists = from s in authDbContext.posts
                            select s;

            var searchResults = postLists.Where(p => p.Content.ToUpper().Contains(searchTerm)
                    || p.Description.ToUpper().Contains(searchTerm)).ToList();

            return PartialView("PostSearchResults", searchResults);

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}