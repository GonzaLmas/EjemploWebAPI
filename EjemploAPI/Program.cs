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


//Son una medida de seguridad importante en la programación web que permite que los navegadores web se comuniquen
//de manera segura con diferentes dominios, siempre que el servidor del recurso lo permita explícitamente. Esto es esencial para
//construir aplicaciones web modernas que dependen de la interacción con servicios o recursos en dominios externos.

var reglasCors = "ReglasCors";  //declaro variable para usar los cors

//dentro de este builder van a estar los cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: reglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//activo los cors
app.UseCors(reglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
