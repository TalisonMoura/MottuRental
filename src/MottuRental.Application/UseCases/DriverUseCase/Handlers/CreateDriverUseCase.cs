using MediatR;
using AutoMapper;
using MottuRental.Domain.Models;
using MottuRental.Application.UseCases.Base;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services;
using MottuRental.Domain.Interfaces.Repository;
using MottuRental.Domain.Core.Notifications.Interfaces;
using MottuRental.Infra.CrossCutting.Commons.Extensions;
using MottuRental.Application.UseCases.DriverUseCase.Request;
using MottuRental.Application.UseCases.DriverUseCase.Response;

namespace MottuRental.Application.UseCases.DriverUseCase.Handlers;

public class CreateDriverUseCase(
    IMapper mapper,
    IMediator mediator,
    IUnitOfWork unitOfWork,
    IDriverService driverService,
    IHandler<DomainNotification> notifications) : UseCaseBase<CreateDriverRequest, CreateDriverReponse>(mapper, mediator, unitOfWork, notifications)
{
    private readonly IDriverService _driverService = driverService;
    private static DateTime HasFullAge => DateTime.UtcNow.AddYears(-18).Date;
    const string driverImagesPath = @"C:\MotturentalDriverImages";

    public override async Task<CreateDriverReponse> HandleSafeMode(CreateDriverRequest request, CancellationToken cancellationToken)
    {
        if (HasFullAge >= request.BirthDate.Date)
        {
            FileHelper.CreateFolder(driverImagesPath);

            var response = await _driverService.RegisterDriverAsync(_mapper.Map<Driver>(request), cancellationToken);

            if (response is null)
            {
                Notifications.Handle(DomainNotification.Error("_001", "This driver has already exist"));
                return default;
            }

            response.SetImageName($"{response.Id}.{request.File.FileName.Split('.')[1]}");
            await FileHelper.SetImageInFolderAsync(driverImagesPath, response.ImagemCNH, await request.File.ToByteAsync());

            await SaveChangesAsync();

            return new CreateDriverReponse { Id = response.Id, IsRegistered = true };
        }

        Notifications.Handle(DomainNotification.Error("_006", "This driver is not full of age"));
        return default;
    }
}