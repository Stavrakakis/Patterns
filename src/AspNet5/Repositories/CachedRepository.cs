namespace AspNet5.Repositories
{
    using Logging;
    using Microsoft.Framework.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    public class CachedRepository<TEntity> : IRepository<TEntity>
    {
        private readonly IRepository<TEntity> repository;
        private readonly ILoggerFactory loggerFactory;

        private readonly static MemoryCache cache = new MemoryCache(new MemoryCacheOptions { });

        public CachedRepository(IRepository<TEntity> repository, ILoggerFactory loggerFactory)
        {
            this.repository = repository;
            this.loggerFactory = loggerFactory;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var loggerContextType = this.repository.GetType();
            var cachedItems = cache.Get<IEnumerable<TEntity>>("thingies");

            var logger = this.loggerFactory.GetLogger(loggerContextType);
            
            if (cachedItems != null)
            {
                logger.Log("Getting thingies from cache");
                return cachedItems;
            }

            MethodBase method = loggerContextType.GetMethod("GetAll");

            var cacheOptions = method.GetCustomAttribute<CacheAttribute>();

            logger.Log("Getting thingies from database");

            var entities = await this.repository.GetAll();
            cache.Set("thingies", entities, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheOptions.ExpiryInMinutes) });

            return entities;
        }
    }

    public class CacheAttribute : Attribute
    {
        private int minutes;

        public CacheAttribute(int minutes)
        {
            this.minutes = minutes;
        }

        public int ExpiryInMinutes
        {
            get
            {
                return minutes;
            }
            set
            {
                this.minutes = value;
            }
        }
    }
}
