namespace JetMovie.Services
{
    public class CacheItem : ICacheItem
    {
        private readonly object _item;

        public CacheItem(object item)
        {
            _item = item;
        }
        public T Get<T>()
        {
            return (T) _item;
        }
    }
}