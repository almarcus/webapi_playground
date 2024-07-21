using Serilog;
using WebAPI.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Logging.AddJsonConsole(options =>
{
	options.IncludeScopes = false;
	options.TimestampFormat = "yyyy-MM-ddThh:mm:ss.ffff";
	options.JsonWriterOptions = new System.Text.Json.JsonWriterOptions { Indented = builder.Environment.IsDevelopment() };
});

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.MapGet("/{name}", (string name) => $"Hello {name}!");

app.UseSerilogRequestLogging();
app.UsePathBase("/api");

app.MapHealthChecks("/healthz");

app.UseExceptionHandler();

// app.UseLambdaServer();

app.Run();
