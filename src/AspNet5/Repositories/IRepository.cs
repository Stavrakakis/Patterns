namespace AspNet5.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
    }
}
