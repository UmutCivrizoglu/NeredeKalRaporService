namespace Application.Report;

public interface IReportService
{
    Task<Guid> SaveReportResultAsync(ReportDetailRequest reportResultDto);
}