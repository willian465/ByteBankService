using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anima.Fiscal.Extensions
{
    /// <summary>
    /// Classe extensao do fluent para facilitar sua execução
    /// </summary>
    public static class FluentValidationExtension
    {
        /// <summary>
        /// Metodo para realizar validação e executar acao programada
        /// </summary>
        public static void Validar<T>(this AbstractValidator<T> validator, T parametro, Action<string[]> action)
        {
            ValidationResult validationResult = validator.Validate(parametro);
            if (!validationResult.IsValid)
            {
                action?.Invoke(validationResult.Errors.ConvertToArrayString());
            }
        }
        /// <summary>
        /// Metodo para realizar validação e executar acao programada
        /// </summary>
        public static string[] ConvertToArrayString(this IList<ValidationFailure> erros)
        {
            return erros.Select(x => { string message = x.ErrorMessage; return message; }).ToArray();
        }
    }
}
