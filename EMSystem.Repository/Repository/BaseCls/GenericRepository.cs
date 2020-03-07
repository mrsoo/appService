using EMSystem.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSystem.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using EMSystem.Data.DbConfig;

namespace EMSystem.Repository.BaseCls
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly WebDbContext context;
        public GenericRepository(WebDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task Create(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            if(entity != null)
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>()
                 .AsNoTracking()
                 .FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task Update(int id, TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
        }
    }

   
}
