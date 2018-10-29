namespace JetMovie.Services
{
    public interface ICacheItem
    {
        T Get<T>();
    }
}