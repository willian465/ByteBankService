using System.Collections.Generic;
using System.Linq;

namespace ByteBank.Service
{
    public class Pai
    {
        public Pai(string nome, int idPai)
        {
            Nome = nome;
            IdPai = idPai;
        }

        public string Nome { get; set; }
        public int IdPai { get; set; }
    }

    public class Filho
    {
        public Filho(string nome, int idFilho, int idPai)
        {
            Nome = nome;
            IdFilho = idFilho;
            IdPai = idPai;
        }

        public string Nome { get; set; }
        public int IdFilho { get; set; }
        public int IdPai { get; set; }

        public void InnerJoinExemplo()
        {
            IEnumerable<Pai> pais = new List<Pai>
            {
                new Pai("José de Freitas", 1),
                new Pai("Carlos dos Santos", 2),
                new Pai("Amarildo de Almeida Silva", 3),
                new Pai("Mário de Campos", 4)
            };

            IEnumerable<Filho> filhos = new List<Filho>
            {
                new Filho("Willian de Carvalho", 1, 1),
                new Filho("Bruno de Prates Silva", 65, 2),
                new Filho("Cleber de Junior andrade", 2, 56)
            };


            IEnumerable<Filho> resultJoin =
                filhos.Join(pais,
                pai => pai.IdPai,
                filho => filho.IdPai,
                (filho, pai) =>
                {
                    filho.IdPai = pai.IdPai;
                    return filho;
                }).GroupBy(x => x.IdFilho).Select(x => x.FirstOrDefault());

            var resultJoin2 =
                filhos.Join(pais,
                pai => pai.IdPai,
                filho => filho.IdPai,
                (filho, pai) => new { filho, pai }).Select(x => new
                {
                    x.filho.IdFilho,
                    x.filho.Nome,
                    NomePai = x.pai.Nome.Replace("Nome", "Nome Pai"),
                    NomeCocatenado = $"O Nome do filho é {x.filho.Nome} e do pai {x.pai.Nome}"
                }).Where(x => x.IdFilho == 1);

            var resultJoin3 =
                filhos.Join(pais,
                pai => pai.IdPai,
                filho => filho.IdPai,
                (filho, pai) => new { filho, pai }).Select(x => new
                {
                    x.filho.IdFilho,
                    x.filho.Nome,
                    NomePai = x.pai.Nome.Replace("Nome", "Nome Pai"),
                    NomeCocatenado = $"O Nome do filho é {x.filho.Nome} e do pai {x.pai.Nome}"
                }).Where(x => x.IdFilho == 1);

        }

    }
}







