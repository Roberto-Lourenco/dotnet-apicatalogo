using APICatalogo.WebAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

builder.Services
    .AddDatabaseConfig(builder.Configuration)
    .AddApiConfiguration()
    .AddRepositories()
    .AddSwaggerConfig();

var app = builder.Build();

await app.UseDataSeederAsync();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseHsts();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
        diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress?.ToString());
    };
});

app.UseSwaggerConfig();
app.UseApplicationLocalization();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
