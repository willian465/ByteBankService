using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IMemoryRepository
    {
        void Remover(string key);
        T Adicionar<T>(T model, string key);
        T Buscar<T>(string key);
        T Atualizar<T>(T model, string key);
    }
}
