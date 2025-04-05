using Wolverine;
using Wolverine.Service;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddWolverine(config =>
{

})
    .AddHostedService<Worker>();


var host = builder.Build();

host.Run();