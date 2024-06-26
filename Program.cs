using Microsoft.EntityFrameworkCore;
using CatalogoAPI.Context;
using System.Text.Json.Serialization;

namespace CatalogoAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options => 
                            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        string? mySqlConnection = builder.Configuration.GetConnectionString("MySqlConnection");

        builder.Services.AddDbContext<CatalogoAPIContext>(options => 
                            options.UseMySql(mySqlConnection, 
                            ServerVersion.AutoDetect(mySqlConnection)));

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
}
