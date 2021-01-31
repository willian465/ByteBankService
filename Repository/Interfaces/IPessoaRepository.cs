﻿using ByteBank.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        int CriarPessoa(PessoaArgument pessoa);
        Task<bool> GerarTipoPessoa(string descricaoTipoPessoa);
        Task<IEnumerable<TipoPessoaModel>> BucarTiposPessoa();
        Task<PessoaModel> BuscarPessoaPorCodigo(int codigoPessoa);
    }
}