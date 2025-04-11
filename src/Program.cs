using McpServer.Tools;
using ModelContextProtocol;
using ModelContextProtocol.Server;

var builder = WebApplication.CreateBuilder(args);

// Add MCP server services
builder.Services.AddMcpServer()
    .WithTools<MultiplicationTool>()
    .WithTools<TemperatureConverterTool>()
    .WithTools<WeatherTools>();

// Add CORS for SSE support in browsers
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS
app.UseCors();

// Map MCP endpoints
app.MapMcp();

// Add a simple home page
app.MapGet("/", () => "MCP Server on Azure App Service - Ready for use with SSE");

app.Run();
