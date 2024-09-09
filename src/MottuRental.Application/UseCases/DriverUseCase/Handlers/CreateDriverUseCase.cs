using MediatR;
using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Providers;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Handlers;

public class CreateDriverUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IDriverService driverService,
    DirectoryProvider directoryProvider,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateDriverRequest, CreateDriverReponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IDriverService _driverService = driverService;
    private readonly DirectoryProvider _directoryProvider = directoryProvider;

    public override async Task<CreateDriverReponse> HandleSafeMode(CreateDriverRequest request, CancellationToken cancellationToken)
    {
        try
        {
            request.ValidateRequest(Notifications);

            if (!Notifications.HasNotification())
            {
                FileHelper.CreateFolder(_directoryProvider.DriverImageFolder);

                var response = await _driverService.RegisterDriverAsync(_mapper.Map<Driver>(request), cancellationToken);

                if (response is null)
                {
                    Notifications.Handle(DomainNotification.Error("_001", "This driver already exist"));
                    return default;
                }

                await FileHelper.SetImageInFolderAsync(_directoryProvider.DriverImageFolder, response.ImagemCNH, await request.File.ToByteAsync());
                return new CreateDriverReponse { Id = response.Id, IsRegistered = await SaveChangesAsync() };
            }
            return default;
        }
        catch (Exception ex)
        {
            Notifications.Handle(DomainNotification.Error("_007", $"{ex.Message}"));
            return default;
        }
    }
}