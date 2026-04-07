using AutoMapper;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Module;
using PlantCare.Queries.Responses.Place;
using PlantCare.Queries.Responses.Plants;
using Module = PlantCare.Queries.Responses.Module.Module;
using Place = PlantCare.Queries.Responses.Place.Place;
using Plant = PlantCare.Queries.Responses.Plants.Plant;

namespace PlantCare.Queries.MapperProfile;

public class QueriesMapperProfile : Profile
{
    public QueriesMapperProfile()
    {
        // PLANT QUERY MAPPINGS
        
        CreateMap<int, GetPlantQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<IPlant, Plant>();
        
        // MODULE MAPPINGS

        CreateMap<Domain.Models.Module.Module, Module>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.isAvailable, opt => opt.MapFrom(src => src.Plant != null));

        // PLACES MAPPINGS

        CreateMap<Domain.Models.Place.Place, IPlace>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Plants, opt => opt.MapFrom(src => src.Plants));

        CreateMap<IPlace, Place>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}