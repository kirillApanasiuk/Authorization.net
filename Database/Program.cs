using System;
using System.Security.Claims;
using Database.Data;
using Database.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Database
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                DatabaseinInitializer.Init(scope.ServiceProvider);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class DatabaseinInitializer
    {
        public static  void Init(IServiceProvider scopeServiceProvider)
        {
            var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser
            {
                UserName = "User",
                LastName = "LastName",
                FirstName = "FirstName"
            };

           var result =  userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
           if (result.Succeeded)
           {
               userManager.AddClaimAsync(user,new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
           }


        }
    }
}
