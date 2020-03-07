using EMSystem.Data.DbConfig;
using EMSystem.Domain.Model;
using EMSystem.Repository.BaseCls;
using EMSystem.Resource.Repository.Repositories.InterfaceDefine;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EMSystem.Resource.Repository.Repositories
{
    public class DpmRepository : GenericRepository<Department>, IDpmRepository
    {
        public DpmRepository(WebDbContext context) : base(context)
        {

        }
        public async Task<Department> getCoolestDepartment()
        {
            return await GetAll().OrderByDescending(d => d.name).FirstOrDefaultAsync();
        }
    }
}
