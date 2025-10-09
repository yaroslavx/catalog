using Catalog.Service.Entities;
using Catalog.Service.Settings;
using Common.MongoDB;
using Common.Settings;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, configurator) =>
    {
        var rabbitMQSettings = builder.Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
        configurator.Host(rabbitMQSettings.Host);
        configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(
            builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>().ServiceName, false));
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddCors();

builder.Services.AddOpenApi();

builder.Services.AddMongo()
    .AddMongoRepository<Item>("items");


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.MapControllers();
 
app.Run();