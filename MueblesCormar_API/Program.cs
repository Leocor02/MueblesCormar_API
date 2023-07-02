using Microsoft.EntityFrameworkCore;
using MueblesCormar_API.Models;
using System.Drawing;

namespace MueblesCormar_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Creación de la configuración de cadena de conexión contra el entorno
            var conn = @"SERVER=.\SQLEXPRESS;DATABASE=MueblesCormar;INTEGRATED SECURITY=TRUE; USER Id=;Password=";
            builder.Services.AddDbContext<MueblesCormarContext>(options => options.UseSqlServer(conn));
    
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}