using ByteBank.Request;
using FluentValidation;

namespace ByteBank.Service.Validador
{
    public class VetorValidator : AbstractValidator<VetorRequest>
    {
        public VetorValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Dados inválidos. Informe os vetores e as combinações");

            RuleFor(x => x.IdVetor)
                .NotEmpty()
                .WithMessage("O codigo do vetor deve ser informado");
        }
    }
}
