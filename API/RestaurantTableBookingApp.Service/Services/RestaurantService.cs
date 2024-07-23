using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Data.IRepositories;
using RestaurantTableBookingApp.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantTableBookingApp.Service.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;
        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }
        public Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
            return restaurantRepository.GetAllRestaurantAsync();
        }

        public Task<IEnumerable<RestaurantBranchModel>> GetAllRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            return restaurantRepository.GetRestaurantBranchesByRestaurantIdAsync(restaurantId);
        }

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId)
        {
            return restaurantRepository.GetDiningTablesByBranchAsync(branchId);
        }

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            return restaurantRepository.GetDiningTablesByBranchAndDateAsync(branchId, date);
        }
        public Task<IEnumerable<MenuModel>> GetRestaurantMenuByRestaurantIdAsync(int restaurantId)
        {
            return restaurantRepository.GetRestaurantMenuByRestaurantIdAsync(restaurantId);
        }
        public Task<IEnumerable<ChefModel>> GetRestaurantChefsByRestaurantIdAsync(int restaurantId)
        {
            return restaurantRepository.GetRestaurantChefsByRestaurantIdAsync(restaurantId);
        }
        public Task<IEnumerable<EventModel>> GetRestaurantEventsByRestaurantIdAsync(int restaurantId)
        {
            return restaurantRepository.GetRestaurantEventsByRestaurantIdAsync(restaurantId);
        }
        public Task<IEnumerable<ReviewModel>> GetRestaurantReviewsByRestaurantIdAsync(int restaurantId)
        {
            return restaurantRepository.GetRestaurantReviewsByRestaurantIdAsync(restaurantId);
        }
    }
}
