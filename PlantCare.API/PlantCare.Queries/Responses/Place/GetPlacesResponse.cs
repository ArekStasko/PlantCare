namespace PlantCare.Queries.Responses.Place;

public record GetPlacesResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}