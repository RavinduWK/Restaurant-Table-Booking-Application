
using Microsoft.EntityFrameworkCore;
using RestaurantTableBookingApp.Data;
using RestaurantTableBookingApp.Data.IRepositories;
using RestaurantTableBookingApp.Data.Repositories;
using RestaurantTableBookingApp.Service.IServices;
using RestaurantTableBookingApp.Service.Services;

namespace RestaurantTableBookingAppAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<IRestaurantService, RestaurantService>();


            builder.Services.AddDbContext<RestaurantTableBookingDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DbContext") ?? "")
            .EnableSensitiveDataLogging() //should not be used for production, only for development purpose
            );

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
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
}
