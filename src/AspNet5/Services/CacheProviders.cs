namespace AspNet5.Services
{
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
}
