

using MassTransit;

namespace Application.Report;

public class ReportProducer
{
    private readonly IBus _bus;

    public ReportProducer(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishMessage(ReportRequest message)
    {
        await _bus.Publish(message);
    }
}

public class ReportRequest
{
    public Guid ReportId { get; set; }
    public string City { get; set; }
}