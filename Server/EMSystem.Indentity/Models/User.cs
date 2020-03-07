using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Indentity.Models
{
    public class User : IdentityUser
    {
        public ICollection<IdentityUserClaim<string>> Claims { get; set; } = new List<IdentityUserClaim<string>>();

    }
}
