using Catalog.Service.Entities;
using Common.MongoDB;

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