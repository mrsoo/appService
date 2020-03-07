using EMSystem.Domain.Model;
using EMSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Resource.Repository.Repositories.InterfaceDefine
{
    public interface IEmpRepository :IGenericRepository<Employee>
    {
        Task<Employee> getCoolestEmployee();
    }
}
