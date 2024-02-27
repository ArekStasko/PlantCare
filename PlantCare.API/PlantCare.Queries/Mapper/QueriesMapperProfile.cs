using AutoMapper;
using PlantCare.Domain.Models.HumidityMeasurement;
using PlantCare.Domain.Models.Module;
using PlantCare.Domain.Models.Place;
using PlantCare.Domain.Models.Plant;
using PlantCare.Queries.Queries.Plant;
using PlantCare.Queries.Responses.Module;
using PlantCare.Queries.Responses.Place;
using PlantCare.Queries.Responses.Plants;

namespace PlantCare.Queries.MapperProfile;

public class QueriesMapperProfile : Profile
{
    public QueriesMapperProfile()
    {
        // PLANT QUERY MAPPINGS
        
        CreateMap<int, GetPlantQuery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

        CreateMap<IPlant, GetPlantResponse>();
        
        // MODULE MAPPINGS

        CreateMap<Module, GetModulesResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Plant, opt => opt.MapFrom(src => src.Plant));

        // PLACES MAPPINGS

        CreateMap<Place, IPlace>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Plants, opt => opt.MapFrom(src => src.Plants));

        CreateMap<IPlace, GetPlacesResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}