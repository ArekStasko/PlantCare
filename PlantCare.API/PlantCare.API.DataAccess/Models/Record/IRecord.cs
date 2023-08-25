namespace PlantCare.API.DataAccess.Models.Record;

public interface IRecord
{
    int PlantId { get; set; }
    byte MoistureLevel { get; set; }
    TimeSpan IrrigationDate { get; set; }
}