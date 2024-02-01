using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EAC.Application.Email;
using EAC.Application.Errors;
using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EAC.Application.Commands.User.CreateTransfer
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailService _emailService;

        public TransferCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<string>> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var reciever = await _unitOfWork.User.GetUserByEmailAsync(request.RecieverEmail);

            if (reciever is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.BadRequest);

            var senderClaimId = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
            var sender = await _unitOfWork.User.GetUserByIdAsync(senderClaimId);

            if (sender is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.BadRequest);

            sender.Balance -= request.Value;
            reciever.Balance += request.Value;

            var transfer = new Transfer
            {
                Amount = request.Value,
                ReceiverId = reciever.Id,
                SenderId = senderClaimId,
            };

            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.Transfer.CreateTransferAsync(transfer);
            await _unitOfWork.User.UpdateUserAsync(sender);
            await _unitOfWork.User.UpdateUserAsync(reciever);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();

            return Result<string>.Success($"Transferencia efetuada no valor de {request.Value}");

        }
    }
}