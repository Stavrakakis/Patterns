namespace AspNet5.Repositories
{
    using Serilog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LoggedRepository<TEntity> : IRepository<TEntity>
    {
        IRepository<TEntity> repository;

        public LoggedRepository(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using (Log.Logger.BeginTimedOperation("repository call"))
            {
                var entities = await this.repository.GetAll();
                return entities;
            }
        }
    }
}
