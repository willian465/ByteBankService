using Anima.Fiscal.Extensions;
using ByteBank.Exceptions;
using ByteBank.Interface;
using ByteBank.Repository.Interfaces;
using ByteBank.Request;
using ByteBank.Response;
using ByteBank.Service.Validador;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class OperacoesMatematicasService : IOperacaoesMatematicasService
    {
        private readonly IOperacoesMatematicasRepository _operacoesMatematicasRepository;
        private readonly ILogger<OperacoesMatematicasService> _logger;

        public OperacoesMatematicasService(IOperacoesMatematicasRepository operacoesMatematicasRepository,
                                           ILogger<OperacoesMatematicasService> logger)
        {
            _operacoesMatematicasRepository = operacoesMatematicasRepository;
            _logger = logger;
        }
        public int SomarDoisNumeros(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0)
                return 0;

            return num1 + num2;
        }

        public int SubtracaoDoisNumeros(int num1, int num2)
        {
            if (num1 <= 0)
            {
                throw new Exception("A subtração resultará em número negativo, operação não permitida");

            }

            int resultado = num1 - num2;
            return resultado;
        }

        public async Task<SimilariadeCossenoResponse> CalcularSimilaridadeDoCosseno(DadosCalcularSimilaridadeDoCossenoRequest dadosSimilariadeCossenoRequests)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            new DadosCalcularSimilaridadeDoCossenoValidator().Validar(dadosSimilariadeCossenoRequests,
                erros =>
                {
                    throw new OperacoesMatematicasException(erros, "Similaridade do Cosseno");
                });

            float somaDaMultiplicacaoDosValoresDosVetores = 0;
            double somaDaPotenciaDosValoresDoPrimeiroVetorDaCombinacao = 0;
            double somaDaPotenciaDosValoresDoSegundoVetorDaCombinacao = 0;

            List<DetatalheSimilariadeCossenoResponse> detatalheSimilariadeDoCosseno = new List<DetatalheSimilariadeCossenoResponse>();
            SimilariadeCossenoResponse similariadeDoCossenoResponse = new SimilariadeCossenoResponse()
            {
                Erros = new List<string>()
            };

            VetorRequest primeiroVetorDaCombinacao = new VetorRequest();
            VetorRequest segundoVetorDaCombinacao = new VetorRequest();

            foreach (CombinacaoRequest combinacao in dadosSimilariadeCossenoRequests.Combinacoes)
            {
                primeiroVetorDaCombinacao = dadosSimilariadeCossenoRequests.Vetores.Where(x => x.IdVetor == combinacao.Item1).FirstOrDefault();
                segundoVetorDaCombinacao = dadosSimilariadeCossenoRequests.Vetores.Where(x => x.IdVetor == combinacao.Item2).FirstOrDefault();

                if (primeiroVetorDaCombinacao.Valores.Count == segundoVetorDaCombinacao.Valores.Count)
                {
                    for (int i = 0; i < primeiroVetorDaCombinacao.Valores.Count; i++)
                    {
                        somaDaMultiplicacaoDosValoresDosVetores += primeiroVetorDaCombinacao.Valores[i] * segundoVetorDaCombinacao.Valores[i];

                        somaDaPotenciaDosValoresDoPrimeiroVetorDaCombinacao += Math.Pow(primeiroVetorDaCombinacao.Valores[i], 2);
                        somaDaPotenciaDosValoresDoSegundoVetorDaCombinacao += Math.Pow(segundoVetorDaCombinacao.Valores[i], 2);
                    }

                    double multiplicacaoDaSqrtDaSomaDasPotencias = Math.Sqrt(somaDaPotenciaDosValoresDoPrimeiroVetorDaCombinacao) * Math.Sqrt(somaDaPotenciaDosValoresDoSegundoVetorDaCombinacao);
                    double similaridadeDoCosseno = somaDaMultiplicacaoDosValoresDosVetores / multiplicacaoDaSqrtDaSomaDasPotencias;

                    // adiciona o resultado na lista de resultados
                    detatalheSimilariadeDoCosseno.Add(new DetatalheSimilariadeCossenoResponse
                    {
                        IdCombinacao = combinacao.IdCombinacao,
                        Resultado = similaridadeDoCosseno
                    });

                    // zera as variáveis para o cáculo da próxima combinação
                    somaDaMultiplicacaoDosValoresDosVetores = 0; somaDaPotenciaDosValoresDoPrimeiroVetorDaCombinacao = 0; somaDaPotenciaDosValoresDoSegundoVetorDaCombinacao = 0;
                }
                else
                {
                    similariadeDoCossenoResponse.Erros.Add($"Número de argumentos diferentes entres os vetores da combinação {combinacao.IdCombinacao}");
                }
            }

            if (similariadeDoCossenoResponse.Erros.Count == 0) { similariadeDoCossenoResponse.Erros.Add("Operações realizadas sem erros"); };

            // resultados
            similariadeDoCossenoResponse.Resultados = detatalheSimilariadeDoCosseno;

            try
            {
                similariadeDoCossenoResponse.CodigoRegistro = await _operacoesMatematicasRepository.RegistrarOperacao(dadosSimilariadeCossenoRequests, detatalheSimilariadeDoCosseno);
            }
            catch (Exception e)
            {
                throw new OperacoesMatematicasException($"Erro ao registrar operação. Msg: {e.Message}.", "Similaridade do cosseno");
            }

            stopwatch.Stop();
            _logger.LogInformation($"Finalizando cálculos de {dadosSimilariadeCossenoRequests.Combinacoes.Count} combinações em {stopwatch.ElapsedMilliseconds} Milliseconds");

            return similariadeDoCossenoResponse;
        }

        public async Task<EquacaoSegundoGrauResponse> CalcularEquacaoSegundoGrau(int a, int b, int c)
        {
            EquacaoSegundoGrauResponse equacaoSegundoGrauResponse = new EquacaoSegundoGrauResponse();
            await Task.Run(() =>
            {
                equacaoSegundoGrauResponse.ValorDeDelta = Math.Pow(b, 2) - 4 * a * c;


                if (equacaoSegundoGrauResponse.ValorDeDelta < 0)
                {
                    throw new OperacoesMatematicasException(string.Format("O valor de Delta deu negativo, operacao cancelada. Delta {0}", equacaoSegundoGrauResponse.ValorDeDelta), "Equacao de Segundo Grau");
                }

                equacaoSegundoGrauResponse.DeltaRaizPositiva = (-b + Math.Sqrt(equacaoSegundoGrauResponse.ValorDeDelta)) / (2 * a);
                equacaoSegundoGrauResponse.DeltaRaizNegativa = (-b - Math.Sqrt(equacaoSegundoGrauResponse.ValorDeDelta)) / (2 * a);
            });

            return equacaoSegundoGrauResponse;
        }
    }
}
