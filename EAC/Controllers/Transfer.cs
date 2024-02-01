using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAC.Application.Commands.User.CreateTransfer;
using EAC.Application.Commands.User.Deposit;
using EAC.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EAC.Controllers
{
    public class Transfer : BaseController
    {
        public Transfer(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(DepositMoneyUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Tranfer")]
        public async Task<IActionResult> Tranfer(TransferCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}