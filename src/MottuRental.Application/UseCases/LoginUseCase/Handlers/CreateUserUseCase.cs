using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Models.AccessControl;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Application.UseCases.LoginUseCase.Request;

namespace MottuRental.Application.UseCases.LoginUseCase.Handlers;

public class CreateUserUseCase(
    IMapper mapper, 
    IMediator mediator, 
    IUnitOfWork unitOfWork,
    IUserService baseService,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateUserRequest, bool>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IUserService _baseService = baseService;

    public override async Task<bool> HandleSafeMode(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var entity = await _baseService.ExecuteQueryAsNoTracking.Where(x => x.UserName.ToUpper().Equals(request.UserName.ToUpper())).FirstOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            Guid manager = Guid.Parse("a29a7c19-0569-402a-b773-1db0d7903cf1");
            Guid financial = Guid.Parse("5a03b605-2c73-4cd3-83cb-b7bd3b1bd265");
            var user = new User(request.UserName, request.Document, request.Password, request.IsAdmin, request.IsAdmin ? manager : financial);
            await _baseService.RegisterAsync(user, cancellationToken);
            await SaveChangesAsync();
            return true;
        }

        Notifications.Handle(DomainNotification.Error("_009", "Invalid opetation"));
        return default;
    }
}