using Microsoft.AspNetCore.Identity;

namespace EMSystem.Indentity.Models
{
    public class Role : IdentityRole
    {
        public Role(string name) : base(name) { }
    }
}
