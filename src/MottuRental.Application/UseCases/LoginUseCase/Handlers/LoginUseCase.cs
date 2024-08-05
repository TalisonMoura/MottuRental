using MediatR;
using AutoMapper;
using System.Security.Claims;
using Secutity = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.LoginUseCase.Request;
using MottuRental.Application.UseCases.LoginUseCase.Response;
using MottuRental.Infra.CrossCutting.Commons.Authentication.Authorization;

namespace MottuRental.Application.UseCases.LoginUseCase.Handlers;

public class LoginUseCase(
    IMapper mapper,
    IMediator mediator,
    IJwtUtils jwtUtils,
    IUnitOfWork unitOfWork,
    IUserService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<LoginRequest, LoginResponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IJwtUtils _jwtUtils = jwtUtils;
    private readonly IUserService _baseService = baseService;

    public override async Task<LoginResponse> HandleSafeMode(LoginRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.ExecuteQueryAsNoTracking
            .Include(x => x.Profile)
            .Where(x => x.UserName.ToUpper().Equals(request.UserName.ToUpper()))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is not null && Secutity.Verify(request.Password, entity.Password))
        {
            var token = _jwtUtils.GenerateJwtToken([
                new("userId", $"{entity.Id}", ClaimValueTypes.String),
                new("document", entity.Document, ClaimValueTypes.String),
                new("role", entity.Profile.Name, ClaimValueTypes.String)]);

            return new LoginResponse(token);
        }

        Notifications.Handle(DomainNotification.Error("_008", "Password or Username invalid"));
        return default;
    }
}