using System;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace Database.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser:IdentityUser<Guid>
    {
      public string FirstName { get; set; }
      public string LastName { get; set; }
    }
}
