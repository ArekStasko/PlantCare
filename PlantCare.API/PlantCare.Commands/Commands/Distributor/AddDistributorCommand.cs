namespace PlantCare.Commands.Commands.Distributor;

public class AddDistributorCommand : IHttpPostCommandId
{
    public string Name { get; init; }
    public int UserId { get; set; }
}