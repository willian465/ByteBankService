using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Interface
{
    public interface IPessoaService
    {
        Task<bool> GerarTipoPessoa(string DescricaoTipoPessoa);
    }
}
