using Microsoft.Data.SqlClient;
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

            //obtenemos la info dela cadena de conexion desde el archivo de configuración
            //appsetting.json, el nombre de la etiqueta es CNNSTR
            var CnnStrBuilder = new SqlConnectionStringBuilder(
                builder.Configuration.GetConnectionString("CNNSTR"));

            string conn = CnnStrBuilder.ConnectionString;

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