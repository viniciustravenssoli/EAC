using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAC.Application.Email;
using EAC.Application.Errors;
using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Domain.Result;
using MediatR;

namespace EAC.Application.Commands.User.Deposit
{
    public class DepositMoneyUserCommandHandler : IRequestHandler<DepositMoneyUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IEmailService _emailService;

        public DepositMoneyUserCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<string>> Handle(DepositMoneyUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.RecieverEmail);

            if (existingUser is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.BadRequest);

            existingUser.Balance += request.Value;

            var deposit = new Transfer
            {
                Amount = request.Value,
                ReceiverId = existingUser.Id,
                SenderId = existingUser.Id
            };

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Transfer.CreateTransferAsync(deposit);
            await _unitOfWork.User.UpdateUserAsync(existingUser);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success($"Deposito efetuado no valor de {request.Value}");
        }
    }
}