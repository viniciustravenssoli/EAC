using EAC.Application.DTOs;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Commands.User.CreateUser
{
    public class CreateUserCommand : IRequest<Result<string>>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AddressDTO? Address { get; set; }
    }
}
