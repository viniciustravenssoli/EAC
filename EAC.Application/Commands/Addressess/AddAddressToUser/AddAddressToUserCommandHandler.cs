using EAC.Application.DTOs;
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

namespace EAC.Application.Commands.Addressess.AddAddressToUser
{
    public class AddAddressToUserCommandHandler : IRequestHandler<AddAddressToUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAddressToUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var address = new Address
            {
                Bairro = request.Address.Bairro,
                Cep = request.Address.Cep,
                Complemento = request.Address.Complemento,
                Localidade = request.Address.Localidade,
                Logradouro = request.Address.Logradouro,
                ApplicationUser = user, 
            };

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Address.CreateAddressAsync(address);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success($"Endereço adicionado com sucesso para o usuário {user.Email}");
        }
    }
}
