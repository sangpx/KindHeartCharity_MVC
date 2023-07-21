using Humanizer.Localisation;
using KindHeartCharity.Data;
using KindHeartCharity.Models.Domain;
using KindHeartCharity.Models.DTO;
using KindHeartCharity.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KindHeartCharity.Controllers
{
    //[Authorize]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAdminManageRepository adminManageRepository;
        private readonly IFileService fileService;

        public AdminController(IAdminManageRepository adminManageRepository,
            IFileService fileService)
        {
            this.adminManageRepository = adminManageRepository;
            this.fileService = fileService;
        }
        public IActionResult GetAll()
        {
            return View();
        }


        public async Task<IActionResult> GetAllPost()
        {
            if (!ModelState.IsValid) { return View(); }

            var posts = await adminManageRepository.GetAllAsync();
            return Ok(posts);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {

            if (!ModelState.IsValid) { return View(); }

            var post = await adminManageRepository.GetByIdAsync(id);
            return Ok(post);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid) { return View(post); }

            if (post.ImageFile != null)
            {
                var fileResult = fileService.SaveImage(post.ImageFile);
                if (fileResult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    RedirectToAction("GetAll", "Admin");
                }
                var imageName = fileResult.Item2;
                post.PostImageURL = imageName;
            }

            await adminManageRepository.CreateAsync(post);

            return RedirectToAction("GetAll", "Admin");
        }



        public async Task<IActionResult> Update(Guid id)
        {
            var dataExisting = await adminManageRepository.GetByIdAsync(id);
            if (dataExisting == null) { return NotFound(); }
            return View(dataExisting);
        }


        public async Task<IActionResult> UpdateConfirm(Guid PostId, string Content, string Description)
        {

            var result = await adminManageRepository.UpdateAsync(PostId, Content, Description);
            return Ok(result);

        }

        //[HttpPut("{postId}")]
        //public async Task<IActionResult> UpdateConfirm(Guid postId, [FromBody] PostUpdateRequestDto postUpdateRequestDto)
        //{
        //    if (!ModelState.IsValid) { return View(postUpdateRequestDto); }

        //    var postDomainModel = new Post
        //    {
        //        Content = postUpdateRequestDto.Content,
        //        Description = postUpdateRequestDto.Description,
        //        PostImageURL = postUpdateRequestDto.PostImageURL,
        //        PostDate = postUpdateRequestDto.PostDate,
        //    };

        //    postDomainModel = await adminManageRepository.UpdateAsync(postId, postDomainModel);

        //    if (postDomainModel == null) { return NotFound(); }

        //    var postDto = new PostUpdateRequestDto
        //    {
        //        Content = postDomainModel.Content,
        //        Description = postDomainModel.Description,
        //        PostImageURL = postDomainModel.PostImageURL,
        //        PostDate = postDomainModel.PostDate,

        //    };

        //    return View(postDto);



        //}

        public async Task<IActionResult> Delete(Guid id)
        {
            var dataExisting = await adminManageRepository.GetByIdAsync(id);
            if (dataExisting == null) { return NotFound(); ; }
            return View(dataExisting);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var deletePost = await adminManageRepository.DeleteAsync(id);

            return Ok(deletePost);
        }

    }
}
