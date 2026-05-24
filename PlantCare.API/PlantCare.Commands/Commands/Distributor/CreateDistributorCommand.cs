namespace PlantCare.Commands.Commands.Distributor;

public class CreateDistributorCommand : IHttpPostCommandId
{
    public string Name { get; init; }
    public int UserId { get; set; }
}