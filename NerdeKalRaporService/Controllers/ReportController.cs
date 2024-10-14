using Application.DTOs;
using Application.Report;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NerdeKalRaporService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    
    private readonly IReportRepository _reportRepository;
    private readonly ReportProducer _reportProducer;

    public ReportController( IReportRepository reportRepository,ReportProducer reportProducer)
    {
       
        _reportRepository = reportRepository;
        _reportProducer = reportProducer;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request)
    {
       
        var report = new Report
        {
            Id = Guid.NewGuid(),
            Location = request.Location,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };
        
        await _reportRepository.AddReportAsync(report);
        await _reportProducer.PublishMessage(new ReportRequest(){ReportId = report.Id, City = request.Location});

        return Ok(new { reportId = report.Id, message = "Rapor talebi kuyruğa eklendi." });
    }
    [HttpGet("status")]
    public async Task<IActionResult> GetAllReportStatuses()
    {
        var reports = await _reportRepository.GetReportsAsync();
        return Ok(reports);
    }
   
}
