using EAC.Application.DTOs;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EAC.Application.Commands.Addressess.AddAddressToUser
{
    public class AddAddressToUserCommand : IRequest<Result<string>>
    {
        [JsonIgnore]
        public string? UserId { get; set; }
        public AddressDTO Address { get; set; }
    }
}
