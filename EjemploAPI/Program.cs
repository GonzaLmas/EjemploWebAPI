//agrego referencias
using Microsoft.EntityFrameworkCore;
using EjemploAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuro el contexto con la cadena de conexión
builder.Services.AddDbContext<DbapiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));


//al traer dos tablas en un request, tengo referenceias cíclicas con los objetos. Con esto le indico que las ignore
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
