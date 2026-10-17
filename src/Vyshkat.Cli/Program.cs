using Microsoft.Extensions.DependencyInjection;
using Vyshkat.Cli;
using Vyshkat.Core.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

var services = new ServiceCollection();
services.AddSingleton<IServiceCenter, ServiceCenter>();
services.AddSingleton<BeginnerPolicy>();
services.AddSingleton<FleetService>();
services.AddTransient<ConsoleApp>();

using var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<ConsoleApp>();
app.SeedDemoData();
app.Run();
