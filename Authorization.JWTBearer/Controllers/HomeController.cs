using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Authorization.JWTBearer.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Arkadi"),
                new Claim(JwtRegisteredClaimNames.Email, "arkadi@email.com"),
            };

            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
            SecurityKey key = new SymmetricSecurityKey(secretBytes);

            var signInCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddMinutes(60),
                signInCredentials
                );

            var value = new JwtSecurityTokenHandler().WriteToken(token);
            ViewBag.Token = value;
            return View();

        }

    }
}
