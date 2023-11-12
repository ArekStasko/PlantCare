using Microsoft.EntityFrameworkCore;
using PlantCare.API.DataAccess.Models;

namespace PlantCare.API.DataAccess.Repositories.ModuleRepository;

using AutoMapper;
using Castle.Core.Logging;
using LanguageExt.Common;
using Microsoft.Extensions.Logging;
using PlantCare.API.DataAccess.Interfaces;
using PlantCare.API.DataAccess.Models.Module;

public class ModuleRepository : IModuleRepository
{
    private readonly IModuleContext _context;
    private readonly ILogger<IModuleRepository> _logger;
    private readonly IMapper _mapper;
    public ModuleRepository(IModuleContext context, ILogger<IModuleRepository> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async ValueTask<Result<bool>> Add(int id)
    {
        try
        {
            var module = new Module()
            {
                Id = id
            };
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Module with {Id} Id was successfully created", id);
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
            var moduleToDelete = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id);

            if (moduleToDelete == null)
            {
                _logger.LogError("There is no module to delete with {Id}", id);
                return new Result<bool>(new NullReferenceException());
            }

            _context.Modules.Remove(moduleToDelete);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully deleted module with {Id} id", id);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<bool>> Update(IModule module)
    {
        try
        {
            var moduleToUpdate = await _context.Modules.SingleOrDefaultAsync(m => m.Id == module.Id);

            if (moduleToUpdate == null)
            {
                _logger.LogError("There is no Module to update with {Id}", module.Id);
                return new Result<bool>(new ArgumentNullException());
            }

            moduleToUpdate.RequiredMoistureLevel = module.RequiredMoistureLevel;
            moduleToUpdate.CriticalMoistureLevel = module.CriticalMoistureLevel;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Module with {Id} successfully updated", module.Id);
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<bool>(e);
        }
    }

    public async ValueTask<Result<IReadOnlyCollection<IModule>>> Get()
    {
        try
        {
            var modules = await _context.Modules.ToListAsync<IModule>();
            _logger.LogInformation("Successfully loaded modules");
            return new Result<IReadOnlyCollection<IModule>>(modules);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IReadOnlyCollection<IModule>>(e);
        }
    }

    public async ValueTask<Result<IModule>> Get(int id)
    {
        try
        {
            var module = await _context.Modules.SingleOrDefaultAsync(m => m.Id == id);

            if (module == null)
            {
                _logger.LogError("There is no module with {Id} id", id);
                return new Result<IModule>(new NullReferenceException());
            }

            _logger.LogInformation("Successfully loaded module with {Id} id", id);
            return new Result<IModule>(module);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return new Result<IModule>(e);
        }
    }
}