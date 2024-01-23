using EAC.Application.Errors;
using EAC.Application.Token;
using EAC.Domain.Interfaces;
using EAC.Domain.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGeneratorDois;


        public LoginUserCommandHandler(IUnitOfWork unitOfWork, ITokenGenerator tokenGeneratorDois)
        {
            _unitOfWork = unitOfWork;
            _tokenGeneratorDois = tokenGeneratorDois;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.User.GetUserByEmailAsync(request.Email);

            if (existingUser is null)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var isCorrect = await _unitOfWork.User.CheckPasswordAsync(existingUser, request.Password);

            if (!isCorrect)
                return Result<string>.Failure(UserErrors.NotFoundUser, ResultStatusCodeEnum.NotFound);

            var token = await _tokenGeneratorDois.GenerateJwtToken(existingUser);

            return Result<string>.Success(token);
        }
    }
}
