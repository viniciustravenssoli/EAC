using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAC.Domain.Result;
using MediatR;

namespace EAC.Application.Commands.User.CreateTransfer
{
    public class TransferCommand : IRequest<Result<string>>
    {
        public string RecieverEmail { get; set; }
        public decimal Value { get; set; }
    }
}