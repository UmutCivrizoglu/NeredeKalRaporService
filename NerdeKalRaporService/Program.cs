using Application.Report;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReportDbContext>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddTransient(typeof(IReportService), typeof(ReportService));



builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("ReportRequest", e =>
        {
            e.ConfigureConsumer<ReportConsumer>(context);
           
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<ReportProducer>();

var app = builder.Build();


//var messageQueueService = app.Services.GetRequiredService<IMessageQueueService>();
//messageQueueService.ConsumeReports();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();