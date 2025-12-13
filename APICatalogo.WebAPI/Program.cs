using APICatalogo.Extensions;
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

app.UseApplicationLocalization();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseSwaggerConfig();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
        diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress?.ToString());
    };
});

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
