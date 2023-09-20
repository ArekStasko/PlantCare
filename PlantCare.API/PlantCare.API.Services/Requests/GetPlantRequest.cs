namespace PlantCare.API.Services.Requests;

public class GetPlantRequest : IHttpGetRequest
{
    public int? Id { get; set; }
}