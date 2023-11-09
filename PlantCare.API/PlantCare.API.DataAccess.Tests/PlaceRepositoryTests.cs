using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.DataAccess.Repositories.PlaceRepository;
using PlantCare.API.DataAccess.Tests.Utils;

namespace PlantCare.API.DataAccess.Tests;

public class PlaceRepositoryTests
{
    private Mock<IPlaceContext> _placeContextMock = Setups.SetupPlaceContext();
    private IMock<ILogger<IPlaceRepository>> _loggerMock = new Mock<ILogger<IPlaceRepository>>();
    private IMapper _mapper = Setups.SetupMapper();
    
        [Test]
    public async Task Create_Should_CreateOnePlace()
    {
        IPlace placeToCreate = new Place()
        {
            Name = "Test Name",
        };
        
        

        var PlaceRepository = new PlaceRepository(_placeContextMock.Object, _mapper, _loggerMock.Object);
        var result = await PlaceRepository.Create(placeToCreate);
        
        _placeContextMock.Verify(dbSet => dbSet.Places.AddAsync(It.IsAny<Place>(), It.IsAny<CancellationToken>()), Times.Once);
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
    public async Task Delete_Should_DeletePlace()
    {
        IPlace placeToDelete = new Place()
        {
            Id = 1,
            Name = "Test Name",
        };

        var mockSet = Setups.GetPlaceMockData();
        _placeContextMock.Setup(_ => _.Places).Returns(mockSet.Object);
        
        var PlaceRepository = new PlaceRepository(_placeContextMock.Object, _mapper, _loggerMock.Object);
        var result = await PlaceRepository.Delete(placeToDelete.Id);
        
        _placeContextMock.Verify(dbSet => dbSet.Places.Remove(It.IsAny<Place>()), Times.Once);
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
    public async Task Update_Should_UpdatePlace()
    {
        IPlace updatedPlace = new Place()
        {
            Id = 1,
            Name = "Test Name UPDATED",
        };

        var mockSet = Setups.GetPlaceMockData();
        _placeContextMock.Setup(_ => _.Places).Returns(mockSet.Object);

        var PlaceRepository = new PlaceRepository(_placeContextMock.Object, _mapper, _loggerMock.Object);
        var result = await PlaceRepository.Update(updatedPlace);
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
    public async Task Get_Should_ReturnPlaces()
    {
        var mockSet = Setups.GetPlaceMockData();
        _placeContextMock.Setup(_ => _.Places).Returns(mockSet.Object);

        var PlaceRepository = new PlaceRepository(_placeContextMock.Object, _mapper, _loggerMock.Object);
        var result = await PlaceRepository.Get();
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