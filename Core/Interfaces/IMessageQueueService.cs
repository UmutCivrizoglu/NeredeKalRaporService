namespace Core.Interfaces;

public interface IMessageQueueService
{
    void PublishReportRequest(Guid reportId, string location);
    void ConsumeReports(); 
}