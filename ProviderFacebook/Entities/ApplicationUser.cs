using System;
using Microsoft.AspNetCore.Identity;

namespace ProviderFacebook.Entities
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
