using EMSystem.Indentity.Models;
using EMSystem.Indentity.ViewModels;
using EMSystem.Server.Shared;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace EMSystem.Indentity.Controllers
{
    //[Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager; //IOC
        private readonly SignInManager<User> signInManager;
        private readonly IIdentityServerInteractionService interactionService;
        

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IIdentityServerInteractionService interactionService,RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.interactionService = interactionService;
        }
       
        [HttpPost]
        [Route("api/account/setRole")]
        public async Task<IActionResult> SetRole([FromBody]SetRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userVeri = await userManager.FindByNameAsync(model.userName);
                if (userVeri != null)
                {
                    if (await roleManager.RoleExistsAsync(model.roleName))
                    {
                        await userManager.AddToRoleAsync(userVeri, model.roleName);
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("api/account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {

            if (string.IsNullOrEmpty(model.Email))
                return this.ErrorResult(ErrorCode.REGISTER_REQUIRED_EMAIL);

            if (string.IsNullOrEmpty(model.Firstname))
                return this.ErrorResult(ErrorCode.REGISTER_REQUIRED_FIRST_NAME);

            if (string.IsNullOrEmpty(model.Lastname))
                return this.ErrorResult(ErrorCode.REGISTER_REQUIRED_LAST_NAME);



            var user = new User() { Email = model.Email, UserName = model.Email };

            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.GivenName,
                ClaimValue = model.Firstname
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.FamilyName,
                ClaimValue = model.Lastname
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.Gender,
                ClaimValue = model.Gender.ToString()
            });
            user.Claims.Add(new IdentityUserClaim<string>()
            {
                ClaimType = JwtClaimTypes.BirthDate,
                ClaimValue = model.BirthDate.ToString("yyyy-MM-dd")
            });

          
            var result = await userManager.CreateAsync(user, model.Password);
            var role = "User";
            if (result.Succeeded)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new Role(role));
                }
                await userManager.AddToRoleAsync(user, role);

                return this.OkResult();
            }
            else
            {
                if (result.Errors.Any(x => x.Code == "DuplicateUserName"))
                {
                    return this.ErrorResult(ErrorCode.REGISTER_DUPLICATE_USER_NAME);
                }
                return this.ErrorResult(ErrorCode.BAD_REQUEST);
            }
        }
        [HttpGet]
        [Route("api/account")]

        public IActionResult getMethod()
        {
            return Ok();
        }
        [HttpGet]
        [Route("api/account/logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await signInManager.SignOutAsync();
            var logoutReq = await interactionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutReq.PostLogoutRedirectUri))
                return Redirect("/");
            return Redirect(logoutReq.PostLogoutRedirectUri);
        }
        [HttpPost]
        [Route("api/account/login")]
      //  [EnableCors]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            
            if (result.Succeeded)
            {
                var user = userManager.FindByNameAsync(model.Email);
                return Ok(new { user });
            }
            else if (result.IsLockedOut)
            {
                return BadRequest();
            }
            return Unauthorized();
        }
    }
}   
