using EAC.Application.Errors;
using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.Email);

            if (existingUser is not null)
                return Result<string>.Failure(UserErrors.UserAlredyExist, ResultStatusCodeEnum.BadRequest);

            var newUser = new ApplicationUser()
            {
                Email = request.Email,
                UserName = request.Username,
                NormalizedUserName = request.Username,
                Addresses = new List<Address>()
            };

            if (request.Address is not null)
            {
                // Adiciona o endereço apenas se informado
                newUser.Addresses.Add(new Address
                {
                    Bairro = request.Address.Bairro,
                    Cep = request.Address.Cep,
                    Complemento = request.Address.Complemento,
                    Localidade = request.Address.Localidade,
                    Logradouro = request.Address.Logradouro,
                    Id = request.Address.Id,
                });
            }

            await _unitOfWork.BeginTransactionAsync();
            var isCreated = await _unitOfWork.User.CreateUserAsync(newUser, request.Password);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            if (!isCreated.Succeeded)
                return Result<string>.Failure(isCreated.Errors.Select(error => new ResultError(error.Code, error.Description)).ToList());

            return Result<string>.Success($"Usuario {newUser.Email}, Registrado com sucesso");
        }
    }
}
