using AutoMapper;
using PlantCare.Commands.Commands.HumidityMeasurements;
using PlantCare.Commands.Commands.Module;
using PlantCare.Commands.Commands.Place;
using PlantCare.Commands.Commands.Plant;
using PlantCare.Domain.Dto;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;

namespace PlantCare.Commands.MapperProfile;

public class CommandsMapperProfile : Profile
{
    public CommandsMapperProfile()
    {
        // DTO MAPPINGS
        CreateMap<Plant, PlantDto>();
        CreateMap<Place, PlaceDto>();
        CreateMap<Module, ModuleDto>();
        CreateMap<HumidityMeasurement, HumidityMeasurementDto>();
            
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

        CreateMap<int, DeletePlantCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        // HUMIDITY MEASUREMENTS MAPPINGS
        CreateMap<AddHumidityMeasurementCommand, HumidityMeasurement>()
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.MeasurementDate, opt => opt.MapFrom(src => src.MeasurementDate));

        CreateMap<HumidityMeasurement, AddHumidityMeasurementCommand>()
            .ForMember(dest => dest.ModuleId, opt => opt.MapFrom(src => src.ModuleId))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.MeasurementDate, opt => opt.MapFrom(src => src.MeasurementDate));

        
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