using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NerdeKalRaporService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMessageQueueService _messageQueueService;
    private readonly IReportRepository _reportRepository;

    public ReportController(IMessageQueueService messageQueueService, IReportRepository reportRepository)
    {
        _messageQueueService = messageQueueService;
        _reportRepository = reportRepository;
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
        _messageQueueService.PublishReportRequest(report.Id, report.Location);

        return Ok(new { reportId = report.Id, message = "Rapor talebi kuyruğa eklendi." });
    }
    [HttpGet("status")]
    public async Task<IActionResult> GetAllReportStatuses()
    {
        var reports = await _reportRepository.GetReportsAsync();
        return Ok(reports);
    }
}
