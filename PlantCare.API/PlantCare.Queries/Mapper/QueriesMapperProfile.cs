using AutoMapper;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Module;

namespace PlantCare.Queries.MapperProfile;

public class QueriesMapperProfile : Profile
{
    public QueriesMapperProfile()
    {
        // PLANT QUERY MAPPINGS
        
        CreateMap<int, GetPlantQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        // MODULE MAPPINGS

        CreateMap<Module, GetModulesResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Plant, opt => opt.MapFrom(src => src.Plant));

        CreateMap<HumidityMeasurement, GetCurrentMoistureResponse>()
            .ForMember(dest => dest.CurrentMoisture, opt => opt.MapFrom(src => src.Humidity))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.MeasurementDate));

        // PLACES MAPPINGS

        CreateMap<Place, IPlace>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Plants, opt => opt.MapFrom(src => src.Plants));

    }
}