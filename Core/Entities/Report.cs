namespace Core.Entities;

public class Report
{
    public Guid Id { get; set; }  
    public string Location { get; set; }  
    public DateTime CreatedAt { get; set; }  
    public DateTime? CompletedAt { get; set; }  
    public string Status { get; set; }  

    public ICollection<ReportDetails> ReportDetails { get; set; }  
}
