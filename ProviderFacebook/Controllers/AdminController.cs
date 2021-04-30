using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProviderFacebook.Data;
using ProviderFacebook.Entities;

namespace ProviderFacebook.Controllers
{
    [Authorize]
    public class AdminController:Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager
            ;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        } 

        [Authorize(Policy = "Administrator")]
        public IActionResult Administrator()
        {
            return View();
        }
        
        [Authorize(Policy = "Manager")]
        public IActionResult Manager()
        {
            return View();
        } 

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var externalProviders = await _signInManager.GetExternalAuthenticationSchemesAsync();
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalProviders = externalProviders
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            //var user = await _context.Users.FirstOrDefaultAsync(u =>
            //    u.Password.Equals(model.Password) && u.UserName.Equals(model.UserName));

            if (user == null)
            {
                ModelState.AddModelError("","user not found");
                return View(model);
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, model.UserName),
            //    new Claim(ClaimTypes.Role,"Administrator")
            //};
            //var claimIdentity = new ClaimsIdentity(claims,"Cookie");
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //await HttpContext.SignInAsync("Cookie", claimPrincipal);
            var result =await _signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }

            return View(model);
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/home");
        }

        public IActionResult ExternalSignIn()
        {
            throw new System.NotImplementedException();
        }
    }
        public class LoginViewModel
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public  string ReturnUrl { get; set; }

            public IEnumerable<AuthenticationScheme> ExternalProviders { get; set; }
        }
}
