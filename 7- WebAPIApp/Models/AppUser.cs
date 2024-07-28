using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace _7__WebAPIApp.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; } = null!;
        public DateTime DateAdded { get; set; }
    }
}