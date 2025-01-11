
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Globalization;
using System.Reflection;
using TouchConsulting.GestorInventario.Application;
using TouchConsulting.GestorInventario.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
string outputTem = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz } {RequestId,13} [{Level:u3}] {Message:lj} {Properties} {NewLine}{Exception}";

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .WriteTo.Console(outputTemplate: outputTem)
                 .ReadFrom.Configuration(configuration)
                 .CreateBootstrapLogger();

builder.Host
    .UseSerilog((context, services, configuration) =>
    {
        configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: outputTem);
    });

configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers();

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    })
    .AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    c.CustomSchemaIds(type => type.FullName);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, true);
    c.DescribeAllParametersInCamelCase();
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration, environment.IsEnvironment("Development"));

builder.Services.AddCors(o => o.AddPolicy("All", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

try
{
    Log.Information("Inicio de Api Interno...");
    var app = builder.Build();

    app.UsePathBase("/api");

    var supportedCultures = new[] { new CultureInfo("es-PE") };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("es-PE"),
        SupportedCultures = supportedCultures,
        FallBackToParentCultures = false
    });
    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("es-PE");


    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty;
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Api Base");
    });

    app.UseCors("All");
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    Log.Information("Deteniendo limpiamente la aplicaci�n Api Interno...");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Se produjo una excepci�n no controlada durante el arranque...");
}
finally
{
    Log.CloseAndFlush();
}
