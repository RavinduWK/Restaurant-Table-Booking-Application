using RestaurantTableBookingApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Service.IServices
{
    public interface IRestaurantService
    {
        Task<List<RestaurantModel>> GetAllRestaurantsAsync();
        Task<IEnumerable<RestaurantBranchModel>> GetAllRestaurantBranchesByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date);
        Task<IEnumerable<MenuModel>> GetRestaurantMenuByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<ChefModel>> GetRestaurantChefsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<EventModel>> GetRestaurantEventsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<ReviewModel>> GetRestaurantReviewsByRestaurantIdAsync(int restaurantId);

    }
}
