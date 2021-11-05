using ByteBank.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace ByteBank.Repository
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Buscar<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public T Adicionar<T>(T model, string key)
        {
            return _memoryCache.Set(key, model);
        }

        public void Remover(string key)
        {
            _memoryCache.Remove(key);
        }
        public T Atualizar<T>(T model, string key)
        {
            _memoryCache.Remove(key);
            return _memoryCache.Set(key, model);
        }
    }
}
