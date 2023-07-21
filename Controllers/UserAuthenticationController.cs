using KindHeartCharity.Models.DTO;
using KindHeartCharity.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace KindHeartCharity.Controllers
{
     public class UserAuthenticationController : Controller
     {
          private readonly IAuthRepository authRepository;

          public UserAuthenticationController(IAuthRepository authRepository)
          {
               this.authRepository = authRepository;
          }


          public IActionResult Login()
          {
               return View();
          }

          /// <summary>
          /// Login
          /// </summary>
          /// <returns></returns>
          [HttpPost]

          public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
          {
               if (!ModelState.IsValid)
               {
                    return View(loginRequestDto);
               }
               var result = await authRepository.LoginAsync(loginRequestDto);
               if (result.StatusCode == 1)
               {
                    return RedirectToAction("Index", "Home");
               }
               else
               {
                    //TempData["msg"] = result.Message;
                    return RedirectToAction(nameof(Login));
               }

          }

          public IActionResult Register()
          {
               return View();
          }


          /// <summary>
          /// Register
          /// </summary>
          /// <returns></returns>
          [HttpPost]
          public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
          {
               if (!ModelState.IsValid) { return View(registerRequestDto); }
               registerRequestDto.Role = "user";
               var result = await authRepository.RegisterAsync(registerRequestDto);
               //TempData["msg"] = result.Message;
               return RedirectToAction(nameof(Login));
          }


          /// <summary>
          /// Register Admin
          /// </summary>
          /// <returns></returns>

          //   public async Task<IActionResult> RegisterAdmin()
          //   {
          //        RegisterRequestDto registerRequestDto = new RegisterRequestDto
          //        {
          //             UserName = "Admin",
          //             Email = "admin@gmail.com",
          //             FirstName = "Admin",
          //             LastName = "Admin",
          //             Password = "Admin@123456"
          //        };
          //        registerRequestDto.Role = "admin";
          //        var result = await authRepository.RegisterAsync(registerRequestDto);

          //        return Ok(result);
          //   }


          /// <summary>
          /// Logout
          /// </summary>
          /// <returns></returns>
          [Authorize]
          public async Task<IActionResult> Logout()
          {
               await authRepository.LogOutAsync();
               return RedirectToAction(nameof(Login));
          }
     }


}
