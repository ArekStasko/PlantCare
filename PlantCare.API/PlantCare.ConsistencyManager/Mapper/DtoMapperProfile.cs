using AutoMapper;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.ConsistencyManager.Mapper;

public class DtoMapperProfile : Profile
{
    public DtoMapperProfile()
    {
        // DTO MAPPINGS
        CreateMap<PlantDto, Plant>();
        CreateMap<PlaceDto, Place>();
        CreateMap<HumidityMeasurementDto, HumidityMeasurement>();
        
        CreateMap<ModuleDto, Module>();
    }
}