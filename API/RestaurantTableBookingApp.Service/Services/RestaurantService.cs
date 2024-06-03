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

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsunc(int branchId, DateTime date)
        {
            return restaurantRepository.GetDiningTablesByBranchAsync(branchId, date);
        }

        public Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsunc(int branchId)
        {
            return restaurantRepository.GetDiningTablesByBranchAsync(branchId);
        }
    }
}
