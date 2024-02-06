using System.Reflection;
using LongRunningWorkerService;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Long-Running API",
        Description = "Coding test for a job application process - no names mentioned, to reduce searchability.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example contact",
            Email = "dummy@example.com",  // example.com is a safe domain to use for examples and tests.
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    var theAssembly = Assembly.GetExecutingAssembly();
    var theAssemblyName = theAssembly.GetName().Name;
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{theAssemblyName}.xml"));
});

// Add long-running service
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

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
