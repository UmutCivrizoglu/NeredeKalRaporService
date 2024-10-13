namespace Application.DTOs;

public class ReportStatusDto
{
    public Guid ReportId { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}