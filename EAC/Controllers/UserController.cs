using EAC.Application.Commands.Addressess.AddAddressToUser;
using EAC.Application.Commands.User.CreateUser;
using EAC.Application.Commands.User.LoginUser;
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
            var userIdToken = User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

            command.UserId = userIdToken;
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
