using creditoauto.Common;
using creditoauto.Domain.Interfaces;
using creditoauto.Domain.Interfaces.Infraestructure;
using creditoauto.Infraestructure.Services;
using creditoauto.Repository.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
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
