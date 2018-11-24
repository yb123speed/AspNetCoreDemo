using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OidcSample.Models
{
    public class ApplicationUser:IdentityUser<int>
    {
        public string Avatar { get; set; }
    }
}
