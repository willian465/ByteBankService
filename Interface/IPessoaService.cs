using System.Threading.Tasks;

namespace ByteBank.Interface
{
    public interface IPessoaService
    {
        Task<bool> GerarTipoPessoa(string DescricaoTipoPessoa);
    }
}
