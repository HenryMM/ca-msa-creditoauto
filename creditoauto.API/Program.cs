using creditoauto.Common;
using creditoauto.Common.Validators;
using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Entity.Models;
using creditoauto.Infraestructure.Services;
using creditoauto.Repository.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddFluentValidation(x =>
    {
        x.ImplicitlyValidateChildProperties = true;
    });
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddTransient<IClienteInfraestructura, ClienteInfraestructura>();
    builder.Services.AddTransient<IPatioInfraestructura, PatioInfraestructura>();
    builder.Services.AddTransient<IMarcaInfraestructura, MarcaInfraestructura>();
    builder.Services.AddTransient<IVehiculoInfraestructura, VehiculoInfraestructura>();
    builder.Services.AddTransient<IEjecutivoInfraestructura, EjecutivoInfraestructura>();
    builder.Services.AddTransient<ISolicitudCreditoInfraestructura, SolicitudCreditoInfraestructura>();
    builder.Services.AddTransient(typeof(IFileHelper<>), typeof(FileHelper<>));
    builder.Services.AddTransient<IFileManager, FileManager>();

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped<IValidator<Vehiculo>, VehiculoValidator>();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

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
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}

public partial class Program { }

