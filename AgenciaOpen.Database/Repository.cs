using AgenciaOpen.Domain;
using Microsoft.EntityFrameworkCore;

namespace AgenciaOpen.Database
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public AgenciaOpenContext Context { get; set; }

        public Repository(AgenciaOpenContext context)
        {
            Context = context;
        }

        public async Task CreateAsync(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Set<T>().Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
