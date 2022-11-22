namespace AgenciaOpen.Database
{
    public interface IRepository<T>
    {
        AgenciaOpenContext Context { get; set; }
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();
    }
}
