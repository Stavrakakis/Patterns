namespace AspNet5.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Product
    {
        public int Id;
        public string Name;
    }

    public class ConcreteCacheProvider : ICacheProvider
    {
        public void AddToCache(string key, object value)
        {
            
        }

        public object GetFromCache(string key)
        {
            return new object(){ };
        }
    }

    public interface ICacheProvider
    {
        object GetFromCache(string key);
        void AddToCache(string key, object value);
    }

    public interface ILogger
    {
        void Log(string message, params object[] args);
    }
    
    public interface IProductRepository
    {
        Task<IEnumerable<string>> GetAll();
    }

    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public Task<IEnumerable<string>> GetAll()
        {
            return Task.FromResult((new string[] { "value1", "value2" }).AsEnumerable());
        }

        public Product GetById(int id)
        {
            return new Product() { Id = id, Name = "Foo " + id.ToString() };
        }
    }

    public class CachingProductRepository : IProductRepository
    {
        IProductRepository repository;
        ICacheProvider cacheProvider;

        public CachingProductRepository(IProductRepository repository, ICacheProvider cp)
        {
            this.repository = repository;
            this.cacheProvider = cp;
        }

        public Task<IEnumerable<string>> GetAll()
        {
            return this.repository.GetAll();
        }
    }

    public class LoggingProductRepository : IProductRepository
    {
        private IProductRepository repository;
        private ILogger logger;

        public LoggingProductRepository(IProductRepository repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public Task<IEnumerable<string>> GetAll()
        {
            return repository.GetAll();
        }
    }
}
