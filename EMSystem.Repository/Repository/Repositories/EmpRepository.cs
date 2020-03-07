using EMSystem.Data.DbConfig;
using EMSystem.Domain.Model;
using EMSystem.Repository.BaseCls;
using EMSystem.Resource.Repository.Repositories.InterfaceDefine;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Resource.Repository.Repositories
{
    public class EmpRepository:GenericRepository<Employee>,IEmpRepository
    {
        
        public EmpRepository(WebDbContext context) : base(context)
        {

        }

        public async Task<Employee> getCoolestEmployee()
        {
            return await GetAll().OrderByDescending(c => c.lstname).FirstOrDefaultAsync();
        }
    }
}
