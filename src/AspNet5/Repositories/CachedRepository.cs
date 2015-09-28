namespace AspNet5.Repositories
{
    using AspNet5.Services;
    using Microsoft.Framework.Caching.Memory;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CachedRepository<TEntity> : IRepository<TEntity>
    {
        IRepository<TEntity> repository;
        ICacheProvider cacheProvider;
        private static MemoryCache cache = new MemoryCache(new MemoryCacheOptions { });

        public CachedRepository(IRepository<TEntity> repository, ICacheProvider cp)
        {
            this.repository = repository;
            this.cacheProvider = cp;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var cachedItems = cache.Get<IEnumerable<TEntity>>("thingies");

            if (cachedItems != null)
            {
                Log.Information("Getting thingies from cache");
                return cachedItems;
            }

            Log.Information("Getting thingies from database");

            var entities = await this.repository.GetAll();
            cache.Set("thingies", entities, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20) });

            return entities;
        }
    }
}
