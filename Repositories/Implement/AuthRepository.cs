using KindHeartCharity.Data;
using KindHeartCharity.Models.Domain;
using KindHeartCharity.Models.DTO;
using KindHeartCharity.Repositories.Implement;
using KindHeartCharity.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace KindHeartCharity.Repositories.Implement
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthRepository(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        /// <summary>
        /// Register
        /// </summary>
        /// <param name="registerRequestDto"></param>
        /// <returns></returns>
        public async Task<Status> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(registerRequestDto.UserName);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "User already exist!";
                return status;
            }
            User user = new User()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerRequestDto.Email,
                UserName = registerRequestDto.UserName,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, registerRequestDto.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }


            //RoleManager
            if (!await roleManager.RoleExistsAsync(registerRequestDto.Role))
                await roleManager.CreateAsync(new IdentityRole(registerRequestDto.Role));


            if (await roleManager.RoleExistsAsync(registerRequestDto.Role))
            {
                await userManager.AddToRoleAsync(user, registerRequestDto.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Status> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(loginRequestDto.UserName);

            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid UserName";
                return status;
            }

            //Match Password
            if (!await userManager.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                status.StatusCode = 0;
                status.Message = "Invalid Password";
                return status;
            }

            var signInResult = await signInManager.PasswordSignInAsync(user, loginRequestDto.Password, false, true);

            if (signInResult.Succeeded)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                status.StatusCode = 1;
                status.Message = "Logged in successfully";
                return status;
            }
            else if (signInResult.IsLockedOut)
            {
                status.StatusCode = 0;
                status.Message = "User is locked out";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in";
            }

            return status;
        }


        /// <summary>
        /// LogOut
        /// </summary>
        /// <returns></returns>
        public async Task LogOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
