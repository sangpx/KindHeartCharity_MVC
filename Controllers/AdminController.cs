using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KindHeartCharity.Controllers
{
    [Authorize]
    //[Authorize(Roles = "admin")]
    //[Area("admin")]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}
