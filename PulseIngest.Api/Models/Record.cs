namespace PulseIngest.Api.Models;

public class Record
{
    public int Id { get; set; }
    public string PatientId { get; set; } = "";
    public string DeviceId { get; set; } = "";
    public string SignalType { get; set; } = "";
    public double Value { get; set; }
    public DateTime RecordedAt { get; set; }
    public string QualityFlag { get; set; } = "";
}