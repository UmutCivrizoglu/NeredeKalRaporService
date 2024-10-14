using Application.DTOs;
using Core.Entities;
using Core.Interfaces;
namespace Application.Report;

public class ReportService : IReportService
{
   
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }
        
    public async Task<Guid> SaveReportResultAsync(ReportDetailRequest reportResultDto)
    {
       

        var reportDetails = new ReportDetails
        {
            ReportId = reportResultDto.ReportId,
            Location = reportResultDto.City,
            PhoneNumberCount = reportResultDto.phoneNumberCount,
            HotelCount = reportResultDto.hotelCount
        };
        var report = await _reportRepository.GetReportByIdAsync(reportResultDto.ReportId);
        report.CompletedAt =  DateTime.UtcNow;
        report.Status = "Done";
        await _reportRepository.UpdateAsync(report);
        await _reportRepository.AddAsync(reportDetails);
        await _reportRepository.SaveChangesAsync();

        return reportDetails.ReportId;
    }
}
