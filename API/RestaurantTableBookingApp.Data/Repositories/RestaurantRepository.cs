using Microsoft.EntityFrameworkCore;
using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Data.IRepositories;


namespace RestaurantTableBookingApp.Data.Repositories
{
    public class RestaurantRepository: IRestaurantRepository
    {
        private readonly RestaurantTableBookingDbContext _dbContext;
        public RestaurantRepository(RestaurantTableBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<RestaurantModel>> GetAllRestaurantAsync()
        {
            var restaurants = _dbContext.Restaurants
                .Select(r => new RestaurantModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    Phone = r.Phone,
                    Email = r.Email,
                    ImageUrl = r.ImageUrl,
                }).ToListAsync();

            return restaurants;
        }
    }
}
