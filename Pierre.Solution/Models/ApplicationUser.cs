using Microsoft.AspNetCore.Identity;
using System;

namespace PierresBakery.Models
{
  public class ApplicationUser : IdentityUser
  {
    // [PersonalData]
    public string ? FullName { get; set; }
    // [PersonalData]
    public DateTime AccountCreatedOn { get; set; }
  }
}