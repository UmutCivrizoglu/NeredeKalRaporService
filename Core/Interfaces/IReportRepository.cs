using Application.DTOs;
using Core.Entities;
namespace Core.Interfaces;

public interface IReportRepository
{
    Task<Report> GetReportByIdAsync(Guid id);  
    Task AddReportAsync(Report report);  
    Task<List<Report>> GetAllReportsAsync();  
    Task SaveChangesAsync();
    Task<object?> GetReportsAsync();
    Task AddAsync(ReportDetails reportDetails);
    Task UpdateAsync(Report report);
    Task<ReportDetailsDto> GetReportResultByIdAsync
        (Guid reportId);
}