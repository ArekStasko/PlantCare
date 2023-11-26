using PlantCare.API.DataAccess.Models.HumidityMeasurement;
using PlantCare.API.DataAccess.Models.Module;
using PlantCare.API.Services.Queries.HumidityMeasurementsQueries;
using PlantCare.API.Services.Requests.HumidityMeasurementCommands;
using PlantCare.API.Services.Requests.ModuleCommands;

namespace PlantCare.API.Services.Mapper;

using AutoMapper;
using PlantCare.API.DataAccess.Models;
using PlantCare.API.DataAccess.Models.Place;
using PlantCare.API.Services.Requests;
using PlantCare.API.Services.Requests.PlaceCommands;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // PLANT MAPPINGS
        CreateMap<CreatePlantCommand, Plant>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PlaceId, opt => opt.MapFrom(src => src.PlaceId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<UpdatePlantCommand, Plant>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.PlaceId, opt => opt.MapFrom(src => src.PlaceId))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<UpdateModuleCommand, Module>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RequiredMoistureLevel, opt => opt.MapFrom(src => src.RequiredMoistureLevel))
            .ForMember(dest => dest.CriticalMoistureLevel, opt => opt.MapFrom(src => src.CriticalMoistureLevel));

        CreateMap<AddHumidityMeasurementCommand, HumidityMeasurement>()
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.MeasurementDate, opt => opt.MapFrom(src => src.MeasurementDate));

        CreateMap<int, DeletePlantCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<Guid, DeleteModuleCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<Guid, GetHumidityMeasurementQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<int, GetPlantQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<int, GetHumidityMeasurementQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        // PLACES MAPPINGS
        CreateMap<CreatePlaceCommand, Place>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<UpdatePlaceCommand, Place>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<int, DeletePlaceCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<Place, IPlace>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Plants, opt => opt.MapFrom(src => src.Plants));

    }
}