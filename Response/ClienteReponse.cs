using ByteBank.Model;

namespace ByteBank.Response
{
    public class ClienteReponse
    {
        public int CodigoPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string CpfPessoa { get; set; }
        public string Sexo { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }

        public static explicit operator ClienteReponse(ClienteModel cliente)
        {
            return new ClienteReponse
            {
                CodigoPessoa = cliente.CodigoPessoa,
                NomePessoa = cliente.NomePessoa,
                CpfPessoa = cliente.CpfPessoa,
                Sexo = cliente.Sexo,
                DataNascimento = cliente.DataNascimento,
                Email = cliente.Email


            };
        }



    }
}

