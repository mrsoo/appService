using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Indentity.ViewModels
{
    public class SetRoleViewModel
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string roleName { get; set; }
    }
}
