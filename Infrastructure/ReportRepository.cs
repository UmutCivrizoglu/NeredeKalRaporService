using Application.DTOs;
using Application.Report;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ReportRepository : IReportRepository
{
    private readonly ReportDbContext _context;

    public ReportRepository(ReportDbContext context)
    {
        _context = context;
    }
    
    public async Task<Report> GetReportByIdAsync(Guid id)
    {
    return await _context.Reports.Include(r=>r.ReportDetails).FirstOrDefaultAsync(r=>r.Id == id);
    }

    public async Task AddReportAsync(Report report)
    {
       await _context.Reports.AddAsync(report);
       await _context.SaveChangesAsync();
    }

    public async Task<List<Report>> GetAllReportsAsync()
    {
       return await _context.Reports.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<object?> GetReportsAsync()
    {
        var reports = await _context.Reports
            .Select(r => new ReportStatusDto
            {
                ReportId = r.Id,
                Location = r.Location,
                Status = r.Status,
                CreatedAt = r.CreatedAt
            }).ToListAsync();

        return reports;
    }

  

  
    
    public async Task AddAsync(ReportDetails reportDetails)
    {
        await _context.ReportDetails.AddAsync(reportDetails);
    }

    public async Task UpdateAsync(Report report)
    {
        _context.Reports.Update(report); 
    }
    public async Task<ReportDetailsDto> GetReportResultByIdAsync(Guid reportId)
    {
  
        var reportDetail = await _context.ReportDetails
            .Include(rd => rd.Report)
            .FirstOrDefaultAsync(rd => rd.Id == reportId); 
        
        if (reportDetail == null)
        {
            return null; 
        }
        if (reportDetail.Report.Status != "Done")
        { 
     
            throw new InvalidOperationException("Rapor henüz tamamlanmadı");
        }
        var reportDto = new ReportDetailsDto
        {
           // Id = reportDetail.Id,
            ReportId = reportDetail.Report.Id, 
            HotelCount = reportDetail.HotelCount,
            Location = reportDetail.Location,
            PhoneNumberCount = reportDetail.PhoneNumberCount,
            Status = reportDetail.Report.Status 
        };

        return reportDto; 
    }
}
