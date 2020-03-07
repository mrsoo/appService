using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EMSystem.Indentity.Models;
using EMSystem.Indentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMSystem.Indentity.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private RoleManager<Role> roleManager;
        public RoleController(RoleManager<Role> roleMgr)
        {
            roleManager = roleMgr;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            var mes = "ModelState is invalid";
            if (ModelState.IsValid)
            {
                if (!await roleManager.RoleExistsAsync(name))
                {
                    var result = await roleManager.CreateAsync(new Role(name));
                    if (result.Succeeded)
                        return Ok(result);
                    else return BadRequest(result);
                } else mes = "Duplicate";
                return BadRequest(new { mes });

            }
            return BadRequest(new { mes });
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IEnumerable<IdentityRole>> Get() => await roleManager.Roles.ToListAsync();

        [Route("getbyID")]
        [Authorize]
        public async Task<IdentityRole> Read(int id)
        {
            return await roleManager.FindByIdAsync(id.ToString());
        }
        [Route("getbyName")]
        public async Task<IdentityRole> Read(string name)
        {
            return await roleManager.FindByIdAsync(name);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var mes = "No role found";
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return Ok(result);
                else return BadRequest(result);
            }
            else
                return BadRequest(new { mes });
        }
        [HttpPut]
        public async Task<IActionResult> Update(string id,[FromBody]Role model)
        {
            var mes = "ModelState is invalid";

            if (ModelState.IsValid)
            {
                mes = "No role found";
                var role = await roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    IdentityResult result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                        return Ok(result);
                    else return BadRequest(result);
                }
                else
                    return BadRequest(new { mes });
            }
            return BadRequest(new { mes });
        }

    }
}
