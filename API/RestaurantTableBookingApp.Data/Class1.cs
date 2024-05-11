using Microsoft.EntityFrameworkCore;

namespace RestaurantTableBookingApp.Data
{
    public class RestaurantTableBookingDbContext: DbContext
    {
        public RestaurantTableBookingDbContext(DbContextOptions<RestaurantTableBookingDbContext> options): base(options)
        {
            
        }
    }
}
