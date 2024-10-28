using OefeningRestaurantMinimalApi.Models;

namespace OefeningRestaurantMinimalApi.Services
{
    public interface IRestaurantService
    {
        Task CreateRestaurant(Restaurant item);
        Task<Restaurant?> UpdateRestaurant(int id, Restaurant item);
        Task<Restaurant?> GetRestaurant(int id);
        Task<List<Restaurant>> GetAllRestaurants();
        Task DeleteRestaurant(int id);
    }
}
