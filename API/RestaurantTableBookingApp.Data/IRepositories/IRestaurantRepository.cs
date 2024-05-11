using RestaurantTableBookingApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Data.IRepositories
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantModel>> GetAllRestaurantAsync();
    }
}
