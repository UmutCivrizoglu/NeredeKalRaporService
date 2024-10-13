using Application.DTOs;
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
    
}