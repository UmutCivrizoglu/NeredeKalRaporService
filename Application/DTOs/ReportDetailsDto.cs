namespace Application.DTOs;

public class ReportDetailsDto
{
    public Guid Id { get; set; }  
    public Guid ReportId { get; set; }  
    public string Location { get; set; } 
    public int HotelCount { get; set; }  
    public int PhoneNumberCount { get; set; }
    public string Status { get; set; }
}