using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
using MassTransit;

namespace Application.Report;

public class ReportConsumer : IConsumer<ReportDetailRequest>
{
    private readonly IReportRepository _reportRepository;
    private readonly IReportService _reportService;

    public ReportConsumer(IReportRepository reportRepository,IReportService reportService)
    {
        _reportRepository = reportRepository;
        _reportService = reportService;
    }
    
    public async Task Consume(ConsumeContext<ReportDetailRequest> context)
    {
        await _reportService.SaveReportResultAsync(context.Message);
    }
}
public class ReportDetailRequest
{
    public Guid ReportId { get; set; }
    public string City { get; set; }
    public int phoneNumberCount { get; set; }
    public int hotelCount { get; set; }
    
}
