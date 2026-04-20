namespace PlantCare.Commands.Commands.Plant;

public class UpdatePlantHumidityValues : IHttpPostCommand
{
    public int UserId { get; set; }
    public int PlantId { get; set; }
    public int minHumidity { get; set; }
    public int maxHumidity { get; set; }
}