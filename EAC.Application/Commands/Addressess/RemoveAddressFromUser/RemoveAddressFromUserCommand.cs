using EAC.Application.DTOs;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Commands.Addressess.RemoveAddressFromUser
{
    public class RemoveAddressFromUserCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public int AddressId { get; set; }
    }
}
