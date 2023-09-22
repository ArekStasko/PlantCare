namespace PlantCare.API.Services.Requests;

public class GetPlantQuery : IHttpGetRequest
{
    public int? Id { get; set; }
}