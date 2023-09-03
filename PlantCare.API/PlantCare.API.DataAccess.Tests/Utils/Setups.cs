using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.Services.Mapper;

namespace PlantCare.API.DataAccess.Tests.Utils;

public class Setups
{
    public static IMapper SetupMapper()
    {
        var autoMapperProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(autoMapperProfile));
        return new Mapper(configuration);
    }

    public static Mock<DataContext> SetupDataContext()
    {
        var dataContextMock = new Mock<DataContext>();
        
        dataContextMock.Setup(_ => _.Plants.AddAsync(It.IsAny<Plant>(), It.IsAny<CancellationToken>()))
            .Returns(ValueTask.FromResult((EntityEntry<Plant>)null)).Verifiable();

        return dataContextMock;
    }
}