using Microsoft.AspNetCore.Http;
using OefeningRestaurantMinimalApi.Models;
using OefeningRestaurantMinimalApi.Services;

namespace OefeningRestaurantMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IRestaurantService, RestaurantService>();

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/restaurants", async (IRestaurantService restaurantService) =>
            {
                var restaurants = await restaurantService.GetAllRestaurants();
                return Results.Ok(restaurants);
            }).WithName("GetRestaurants").WithTags("Restaurants");

            app.MapGet("/restaurants/{id}", async (IRestaurantService restaurantService, int id) =>
            {
                var restaurant = await restaurantService.GetRestaurant(id);
                return restaurant == null ? Results.NotFound() : Results.Ok(restaurant);
            }).WithName("GetRestaurant").WithTags("Restaurants");

            app.MapPost("/restaurants", async (IRestaurantService restaurantService, Restaurant item) =>
            {
                await restaurantService.CreateRestaurant(item);
                return Results.Created($"/restaurants/{item.Id}", item);
            }).WithName("CreateRestaurant").WithTags("Restaurants");

            app.MapPut("/restaurants/{id}", async (IRestaurantService restaurantService, int id, Restaurant item) =>
            {
                if (id != item.Id)
                {
                    return Results.BadRequest();
                }

                var updatedRestaurant = await restaurantService.UpdateRestaurant(id, item);
                return updatedRestaurant == null ? Results.NotFound() : Results.Ok(updatedRestaurant);
            }).WithName("UpdateRestaurant").WithTags("Restaurants");

            app.MapDelete("/restaurants/{id}", async (IRestaurantService restaurantService, int id) =>
            {
                try
                {
                    await restaurantService.DeleteRestaurant(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            }).WithName("DeleteRestaurant").WithTags("Restaurants");

            app.Run();
        }
    }
}
