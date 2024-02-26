using AutoMapper;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.ReadModels.HumidityMeasurement;
using PlantCare.Domain.Models.ReadModels.Place;
using PlantCare.Domain.Models.ReadModels.Plant;

namespace PlantCare.ConsistencyManager.Mapper;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        // DTO MAPPINGS
        CreateMap<PlantDto, Plant>()
            .ForMember(p => p.ConsistencyId, opts => opts.MapFrom(d => d.Id));
        CreateMap<PlaceDto, Place>()
            .ForMember(p => p.ConsistencyId, opts => opts.MapFrom(d => d.Id));
        CreateMap<HumidityMeasurementDto, HumidityMeasurement>()
            .ForMember(p => p.ConsistencyId, opts => opts.MapFrom(d => d.Id));
        
        CreateMap<ModuleDto, Module>();
    }
}