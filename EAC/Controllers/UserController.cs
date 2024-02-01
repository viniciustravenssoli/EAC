using EAC.Application.Commands.Addressess.AddAddressToUser;
using EAC.Application.Commands.Addressess.RemoveAddressFromUser;
using EAC.Application.Commands.User.CreateUser;
using EAC.Application.Commands.User.LoginUser;
using EAC.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EAC.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Add-Address")]
        public async Task<IActionResult> AddAddress(AddAddressToUserCommand command)
        {
            var x = User.Identity.Name;
            var userIdToken = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

            command.UserId = userIdToken;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("removeAddress/{addressId}")]
        public async Task<IActionResult> RemoveAddress([FromRoute] int addressId)
        {
            var userIdToken = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

            var command = new RemoveAddressFromUserCommand();

            command.UserId = userIdToken;
            command.AddressId = addressId;

            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.GetFinalObject());
        }
    }
}
