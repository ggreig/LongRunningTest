using LongRunningWorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<LongRunningWorker>();

var host = builder.Build();
host.Run();
