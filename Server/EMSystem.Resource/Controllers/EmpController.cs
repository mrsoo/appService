using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMSystem.Domain.Model;
using EMSystem.Resource.Repository.Repositories;
using EMSystem.Resource.Repository.Repositories.InterfaceDefine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMSystem.Resource.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "User")]
    [Authorize]
    public class EmpController : ControllerBase
    {
        private readonly IEmpRepository repos;

        public EmpController(IEmpRepository repos)
        {
            this.repos = repos;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> Get()
        {
            return await repos.GetAll().ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await repos.GetById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody]Employee model)
        {
            await repos.Create(model);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]Employee model)
        {
            await repos.Update(id, model);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await repos.Delete(id);
        }
    }
}
