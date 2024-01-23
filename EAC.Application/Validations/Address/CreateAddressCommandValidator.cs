using EAC.Application.Commands.Addressess.AddAddressToUser;
using EAC.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Validations.Addressess
{
    public class CreateAddressCommandValidator : AbstractValidator<AddAddressToUserCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Address.Cep)
                .NotEmpty().WithMessage("O CEP não pode estar vazio.")
                .Matches(@"^\d{5}-?\d{3}$").WithMessage("Formato de CEP inválido. Use '12345-678' ou '12345678'.");

            RuleFor(x => x.Address.Logradouro)
                .NotEmpty().WithMessage("O logradouro não pode estar vazio.");

            RuleFor(x => x.Address.Bairro)
                .NotEmpty().WithMessage("O bairro não pode estar vazio.");

            RuleFor(x => x.Address.Localidade)
                .NotEmpty().WithMessage("A localidade não pode estar vazia.");
        }
    }
}
