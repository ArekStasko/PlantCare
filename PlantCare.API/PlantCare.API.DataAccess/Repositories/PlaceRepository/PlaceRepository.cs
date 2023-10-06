using AutoMapper;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Place;

namespace PlantCare.API.DataAccess.Repositories.PlaceRepository;

public class PlaceRepository : IPlaceRepository
{
    private PlaceContext _context;
    private IMapper _mapper;
    private ILogger<PlaceRepository> _logger;

    public PlaceRepository(PlaceContext context, IMapper mapper, ILogger<PlaceRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async ValueTask<Result<bool>> Create(IPlace place)
    {
        try
        {
            await _context.Places.AddAsync((Place)place);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully created new place with {placeId} Id", place.Id);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<bool>> Delete(int id)
    {
        try
        {
            var placeToDelete = await _context.Places.SingleOrDefaultAsync(place => place.Id == id);

            if (placeToDelete == null)
            {
                _logger.LogError("There is no plant to delete with {plantId} Id", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Places.Remove(placeToDelete);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Place with {placeId} successfully deleted", id);
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<bool>> Edit(IPlace place)
    {
        try
        {
            var placeToEdit = await _context.Places.SingleOrDefaultAsync(plc => plc.Id == place.Id);

            if (placeToEdit == null)
            {
                _logger.LogError("There is no place to edit with {plantId} Id", place.Id);
                return new Result<bool>(new NullReferenceException());
            }

            _mapper.Map(place, placeToEdit);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Place with {placeId} successfully edited", place.Id);

            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<List<IPlace>>> Get()
    {
        try
        {
            var places = await _context.Places.ToListAsync<IPlace>();
            _logger.LogInformation("Successfull get opreation");
            return new Result<List<IPlace>>(places);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<List<IPlace>>(e);
        }
    }

}