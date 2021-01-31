using System;

namespace ByteBank.Model
{
    public class PessoaModel
    {
        public int CodigoPessoa { get; set; }
        public string DescricaoTipoPessoa { get; set; }
        public string NomePessoa { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCpfCnpj { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
    }
}
