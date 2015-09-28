namespace AspNet5.Repositories
{
    using Serilog;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PerformanceLoggingRepository<TEntity> : IRepository<TEntity>
    {
        private readonly IRepository<TEntity> repository;
        
        public PerformanceLoggingRepository(IRepository<TEntity> repository)
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
