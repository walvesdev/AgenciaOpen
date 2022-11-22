using AgenciaOpen.Services.Resquest.Commands;
using FluentValidation;

namespace AgenciaOpen.Services.Validators
{
    public class CreateUpdateClienteCommandValidator : AbstractValidator<CreateUpdateClienteCommand>
    {
        public CreateUpdateClienteCommandValidator()
        {
            RuleFor(a => a.Cliente)
                    .NotNull()
                    .WithMessage(s => $"Necessário informar {nameof(s.Cliente)}!");

        }
    }
}
