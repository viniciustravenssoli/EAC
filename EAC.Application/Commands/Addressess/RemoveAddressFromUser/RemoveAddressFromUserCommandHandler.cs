using EAC.Application.Errors;
using EAC.Domain.Interfaces;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Commands.Addressess.RemoveAddressFromUser
{
    public class RemoveAddressFromUserCommandHandler : IRequestHandler<RemoveAddressFromUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAddressFromUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(RemoveAddressFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.User.GetUserByIdAsync(request.UserId);

            if (user is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var address = await _unitOfWork.Address.GetAddressByIdAsync(request.AddressId);

            if (address is null)
                return Result<string>.Failure(AddressErrors.NotFoundAddress, ResultStatusCodeEnum.NotFound);

            if (address.ApplicationUser != user)
                return Result<string>.Failure(AddressErrors.AddressNotBelongToUser, ResultStatusCodeEnum.BadRequest);

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Address.DeleteAddressAsync(address);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success($"Endereço removido com sucesso do usuário {user.Email}");
        }
    }
}
