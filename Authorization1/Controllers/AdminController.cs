using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authorization1.Controllers
{
    [Authorize]
    public class AdminController:Controller
    {
        public IActionResult Index()
        {
            return View();
        } 

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim("demo", "value")
            };
            var claimIdentity = new ClaimsIdentity(claims,"Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);
            return Redirect(model.ReturnUrl);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/home");
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
        }
}
