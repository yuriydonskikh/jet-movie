using System;

namespace JetMovie.Services
{
    public interface ICacheController
    {
        TItem Use<TItem>(string key, Func<TItem> builder);
        void Invalidate(string key);
    }
}