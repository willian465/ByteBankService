using ByteBank.Request;
using FluentValidation;

namespace ByteBank.Service.Validador
{
    public class DadosCalcularSimilaridadeDoCossenoValidator : AbstractValidator<DadosCalcularSimilaridadeDoCossenoRequest>
    {
        public DadosCalcularSimilaridadeDoCossenoValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Request inválido");

            /*RuleForEach(x => x.Vetores)
                .NotEmpty()
                .WithMessage("Lista de vetores deve ser informada")
                .SetValidator(new VetorValidator())*/
        }
    }
}
