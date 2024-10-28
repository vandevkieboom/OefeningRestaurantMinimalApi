using OefeningRestaurantMinimalApi.Models;

namespace OefeningRestaurantMinimalApi.Services
{
    public class RestaurantService : IRestaurantService
    {
        private static readonly List<Restaurant> _allRestaurants = new();

        public Task CreateRestaurant(Restaurant item)
        {
            _allRestaurants.Add(item);
            return Task.CompletedTask;
        }

        public Task DeleteRestaurant(int id)
        {
            var restaurant = _allRestaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is not null)
            {
                _allRestaurants.Remove(restaurant);
            }
            return Task.CompletedTask;
        }

        public Task<List<Restaurant>> GetAllRestaurants()
        {
            return Task.FromResult(_allRestaurants);
        }

        public Task<Restaurant?> GetRestaurant(int id)
        {
            var restaurant = _allRestaurants.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(restaurant);
        }

        public Task<Restaurant?> UpdateRestaurant(int id, Restaurant item)
        {
            var restaurant = _allRestaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant is not null)
            {
                restaurant.Name = item.Name;
                restaurant.Location = item.Location;
                restaurant.Rating = item.Rating;
                restaurant.CuisineType = item.CuisineType;
            }
            return Task.FromResult(restaurant);
        }
    }
}
