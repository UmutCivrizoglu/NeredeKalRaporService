using System.Text;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace Infrastructure;

 public class RabbitMQMessageQueueService : IMessageQueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQMessageQueueService()
        {
            
            var factory = new ConnectionFactory() { HostName = "localhost",UserName = "guest",    
                Password = "guest"  };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            
            _channel.QueueDeclare(queue: "reportQueue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

     
        public void PublishReportRequest(Guid reportId, string location)
        {
            var message = $"{reportId}:{location}";
            var body = Encoding.UTF8.GetBytes(message);

          
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true; 

            _channel.BasicPublish(exchange: "",
                                  routingKey: "ReportQueue",  
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine($"Published report request to queue: {message}");
        }

       
        public void ConsumeReports()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

              
                var reportRequest = ParseMessage(message);

                
                Console.WriteLine($"Received report request: {message}");
                
                await ProcessReport(reportRequest.ReportId, reportRequest.Location);
                
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            
            _channel.BasicConsume(queue: "reportQueue",
                                  autoAck: false, 
                                  consumer: consumer);

            Console.WriteLine("Started listening for report requests...");
        }
        
        private (Guid ReportId, string Location) ParseMessage(string message)
        {
            var parts = message.Split(':');
            var reportId = Guid.Parse(parts[0]);
            var location = parts[1];
            return (reportId, location);
        }
        
        private async Task ProcessReport(Guid reportId, string location)
        {
            Console.WriteLine($"Processing report for location: {location}, Report ID: {reportId}");
            
            await Task.Delay(2000); 

            Console.WriteLine($"Report for {location} has been processed.");
        }
    }