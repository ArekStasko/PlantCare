using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Repositories.HumidityMeasurementRepository;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class HumidityMeasurementsRepositoryTests
{
    private Mock<IHumidityMeasurementContext> _humidityMeasurementContextMock = Setups.SetupHumidityMeasurementsContext();
    private IMock<ILogger<HumidityMeasurementRepository>> _loggerMock = new Mock<ILogger<HumidityMeasurementRepository>>();
    private IMapper _mapper = Setups.SetupMapper();
    private IDistributedCache _cache = Setups.SetupCache();
    
    [Test]
    public async Task Add_Should_AddHumidityMeasurement()
    {
        IHumidityMeasurement humidityMeasurementToAdd = new HumidityMeasurement()
        {
            Humidity = 80,
        };
        
        var humidityMeasurementRepository = new HumidityMeasurementRepository(_humidityMeasurementContextMock.Object, _loggerMock.Object, _cache);
        var result = await humidityMeasurementRepository.Add(humidityMeasurementToAdd);
        
        _humidityMeasurementContextMock.Verify(dbSet => dbSet.HumidityMeasurements.AddAsync(It.IsAny<HumidityMeasurement>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Match<IActionResult>(succ =>
        {
            Assert.IsTrue(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNull(err);
            return new EmptyResult();
        });
    }
    
    [Test]
    public async Task Get_Should_ReturnHumidityMeasurements()
    {
        var mockSet = Setups.GetHumidityMeasurementMockData();
        _humidityMeasurementContextMock.Setup(_ => _.HumidityMeasurements).Returns(mockSet.Object);

        var PlaceRepository = new HumidityMeasurementRepository(_humidityMeasurementContextMock.Object, _loggerMock.Object, _cache);
        var result = await PlaceRepository.Get(new Guid());
        result.Match<IActionResult>(succ =>
        {
            Assert.IsNotNull(succ);
            return new EmptyResult();
        }, err =>
        {
            Assert.IsNull(err);
            return new EmptyResult();
        });
    }
}