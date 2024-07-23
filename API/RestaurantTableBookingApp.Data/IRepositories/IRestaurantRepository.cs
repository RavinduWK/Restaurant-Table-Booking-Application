using RestaurantTableBookingApp.Core;
using RestaurantTableBookingApp.Core.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Data.IRepositories
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantModel>> GetAllRestaurantAsync();
        Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date);
        Task<RestaurantReservationDetails> GetRestaurantReservationDetailsAsync(int timeSlotId);
        Task<User?> GetUserAsync(string emailId);
        Task<IEnumerable<MenuModel>> GetRestaurantMenuByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<ChefModel>> GetRestaurantChefsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<EventModel>> GetRestaurantEventsByRestaurantIdAsync(int restaurantId);
        Task<IEnumerable<ReviewModel>> GetRestaurantReviewsByRestaurantIdAsync(int restaurantId);
    }
}
