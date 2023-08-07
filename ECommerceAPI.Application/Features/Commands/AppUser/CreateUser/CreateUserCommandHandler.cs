using System;
using E = ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Application.Exceptions;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly UserManager<E.AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<E.AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid().ToString(),
            NameSurname = request.Name + " " + request.Surname,
            UserName = request.Username,
            Email = request.Email
        }, request.Password);

        CreateUserCommandResponse response = new()
        {
            Succeeded = result.Succeeded
        };

        if (result.Succeeded)
            response.Message = "You've sucessfully registered.";
        else
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code}";
            }

        return response;
        // throw new UserCreateFailedException();
    }
}

