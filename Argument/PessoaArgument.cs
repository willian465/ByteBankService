using System;

namespace ByteBank
{
    public class PessoaArgument
    {
        public int CodigoTipoPessoa { get; set; }
        public string NomePessoa { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCpfCnpj { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
    }
}