using MyGrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.MigrateDatabase<Program>();
        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).UseSerilog(Logging.ConfigureLogger);

             services.AddMassTransit(x =>
                {
                    x.AddConsumer<DiscountNotificationConsumer>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(context.Configuration["RabbitMQ:HostName"], h =>
                        {
                            h.Username(context.Configuration["RabbitMQ:UserName"]);
                            h.Password(context.Configuration["RabbitMQ:Password"]);
                        });

                        cfg.ReceiveEndpoint("discount_queue", e =>
                        {
                            e.ConfigureConsumer<DiscountNotificationConsumer>(context);
                        });
                    });
                });

                services.AddMassTransitHostedService();
                builder.services.AddScoped<IDiscountPublisher,DiscountPublisher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
