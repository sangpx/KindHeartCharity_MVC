using KindHeartCharity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KindHeartCharity.Controllers
{
    public class PostController : Controller
    {
        private readonly AuthDbContext authDbContext;

        public PostController(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
