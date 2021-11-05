using ByteBank.Request;
using FluentValidation;

namespace ByteBank.Service.Validador
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequest>
    {
        public UsuarioValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Dados inválidos. Informe os vetores e as combinações");

            RuleFor(x => x.Codigo)
                .NotEmpty()
                .WithMessage("O codigo do usuario deve ser informado");
        }
    }
}
