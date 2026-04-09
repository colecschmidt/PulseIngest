using Microsoft.AspNetCore.Mvc;
using PulseIngest.Api.Data;
using PulseIngest.Api.Dtos;
using PulseIngest.Api.Models;

namespace PulseIngest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase
{
    private readonly AppDbContext _db;

    public RecordsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecord([FromBody] CreateRecordRequest request)
    {
        var record = new Record
        {
            PatientId = request.PatientId,
            DeviceId = request.DeviceId,
            SignalType = request.SignalType.ToLower(),
            Value = request.Value,
            RecordedAt = request.RecordedAt.ToUniversalTime(),
            QualityFlag = GetQualityFlag(request.Value)
        };

        _db.Records.Add(record);
        await _db.SaveChangesAsync();

        return Ok(record);
    }

    [HttpGet("{patientId}")]
    public IActionResult GetByPatientId(string patientId)
    {
        var records = _db.Records
            .Where(r => r.PatientId == patientId)
            .OrderBy(r => r.RecordedAt)
            .ToList();

        return Ok(records);
    }

    private static string GetQualityFlag(double value)
    {
        if (value < 20) return "LOW";
        if (value > 100) return "HIGH";
        return "NORMAL";
    }
}