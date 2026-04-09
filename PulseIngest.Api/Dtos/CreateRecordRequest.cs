namespace PulseIngest.Api.Dtos;

public class CreateRecordRequest
{
    public string PatientId { get; set; } = "";
    public string DeviceId { get; set; } = "";
    public string SignalType { get; set; } = "";
    public double Value { get; set; }
    public DateTime RecordedAt { get; set; }
}