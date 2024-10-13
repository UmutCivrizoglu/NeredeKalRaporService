namespace Core.Entities;

public class ReportDetails
{
    public Guid Id { get; set; }  
    public Guid ReportId { get; set; }  
    public string Location { get; set; } 
    public int HotelCount { get; set; }  
    public int PhoneNumberCount { get; set; }  

    public Report Report { get; set; }  
}
