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
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsunc(int branchId, DateTime date);
        Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsunc(int branchId);

    }
}
