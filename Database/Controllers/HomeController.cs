﻿using Microsoft.AspNetCore.Mvc;

namespace Database.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = "Without name";
            ViewBag.IsAuthenticated = false;
            if (User.Identity != null)
            {
                ViewBag.Name = User.Identity.Name;
                ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            }


            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
