using Catalog.Service.Entities;
using Catalog.Service.Repositories;
using Catalog.Service.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

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