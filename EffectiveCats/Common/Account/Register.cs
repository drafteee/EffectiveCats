using AutoMapper;
using BL.Exceptions;
using DAL.Models.Account;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Common.Account
{
    public class Register
    {
        public class Command : IRequest<bool>
        {
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Register> _logger;
            private readonly UserManager<User> _userManager;


            public Handler(ILogger<Register> logger, IMapper mapper, UserManager<User> userManager)
            {
                _logger = logger;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"Register {command.UserName} [{DateTime.Now}]");

                    var userExists = await _userManager.FindByNameAsync(command.UserName);
                    if (userExists != null)
                        throw new AppException("User already exists!");

                    var entity = _mapper.Map<Command, User>(command);

                    var result = await _userManager.CreateAsync(entity, command.Password);

                    if (!result.Succeeded)
                        throw new AppException("User creation failed! Please check user details and try again.");

                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Register error {command.UserName} [{DateTime.Now}]");
                    throw e;
                }
            }
        }
    }
}
