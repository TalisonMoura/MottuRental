using MediatR;
using AutoMapper;
using MottuRental.Domain.Core.Messages;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Application.UseCases.Base;

public abstract class UseCaseBase<TReq, TResp> : IRequestHandler<TReq, TResp> where TReq : CommandRequest<TResp>
{
    protected readonly IMapper _mapper;
    protected readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    protected IHandler<DomainNotification> Notifications { get; }

    protected UseCaseBase(IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork, IHandler<DomainNotification> notifications)
    {
        _mapper = mapper;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        Notifications = notifications;
    }

    protected async Task<bool> SaveChangesAsync()
    {
        if (Notifications.HasNotification())
            return false;

        return await _unitOfWork.SaveChangesAsync();
    }

    public abstract Task<TResp> HandleSafeMode(TReq request, CancellationToken cancellationToken);

    public async Task<TResp> Handle(TReq request, CancellationToken cancellationToken) => await HandleSafeMode(request, cancellationToken);
}