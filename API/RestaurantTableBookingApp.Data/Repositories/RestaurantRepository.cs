using Microsoft.EntityFrameworkCore;
using RestaurantTableBookingApp.Core;
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
        
        public async Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _dbContext.RestaurantBranches
                .Where(rb => rb.RestaurantId == restaurantId)
                .Select(rb => new RestaurantBranchModel()
                {
                    Id = rb.Id,
                    RestaurantId = rb.RestaurantId,
                    Name = rb.Name,
                    Address = rb.Address,
                    Phone = rb.Phone,
                    Email = rb.Email,
                    ImageUrl = rb.ImageUrl,
                }).ToListAsync();

            return branches;
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAsync(int branchId)
        {
            var diningTables = await _dbContext.DiningTables
                .Where(dt => dt.RestaurantBranchId == branchId)
                .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
                {
                    dt.RestaurantBranchId,
                    dt.TableName,
                    dt.Capacity,
                    ts.ReservationDay,
                    ts.MealType,
                    ts.TableStatus,
                    ts.Id
                })
                .OrderBy(ts => ts.Id)
                .ThenBy(ts => ts.MealType)
                .ToListAsync();


            return diningTables.Select(dt => new DiningTableWithTimeSlotsModel
            {
                BranchId = dt.RestaurantBranchId,
                ReservationDay = dt.ReservationDay,
                TableName = dt.TableName,
                Capacity = dt.Capacity,
                MealType = dt.MealType,
                TableStatus = dt.TableStatus,
                TimeSlotId = dt.Id,
                UserEmailId = _dbContext.Reservations
                    .Where(r => r.TimeSlotId == dt.Id)
                    .Join(_dbContext.Users, r => r.UserId, u => u.Id, (r, u) => u.Email)
                    .FirstOrDefault()
            });
        }

        public async Task<IEnumerable<DiningTableWithTimeSlotsModel>> GetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _dbContext.DiningTables
                .Where(dt => dt.RestaurantBranchId == branchId)
                .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
                {
                    dt.RestaurantBranchId,
                    dt.TableName,
                    dt.Capacity,
                    ts.ReservationDay,
                    ts.MealType,
                    ts.TableStatus,
                    ts.Id
                })
                .Where(ts => ts.ReservationDay == date.Date)
                .OrderBy(ts => ts.Id)
                .ThenBy(ts => ts.MealType)
                .ToListAsync();


            return diningTables.Select(dt => new DiningTableWithTimeSlotsModel
            {
                BranchId = dt.RestaurantBranchId,
                ReservationDay = dt.ReservationDay,
                TableName = dt.TableName,
                Capacity = dt.Capacity,
                MealType = dt.MealType,
                TableStatus = dt.TableStatus,
                TimeSlotId = dt.Id
            });
        }

        public async Task<RestaurantReservationDetails> GetRestaurantReservationDetailsAsync(int timeSlotId)
        {

            var query = await (from diningTable in _dbContext.DiningTables
                               join restaurantBranch in _dbContext.RestaurantBranches on diningTable.RestaurantBranchId equals restaurantBranch.Id
                               join restaurant in _dbContext.Restaurants on restaurantBranch.RestaurantId equals restaurant.Id
                               join timeSlot in _dbContext.TimeSlots on diningTable.Id equals timeSlot.DiningTableId
                               where timeSlot.Id == timeSlotId
                               select new RestaurantReservationDetails()
                               {
                                   RestaurantName = restaurant.Name,
                                   BranchName = restaurantBranch.Name,
                                   Address = restaurantBranch.Address,
                                   TableName = diningTable.TableName,
                                   Capacity = diningTable.Capacity,
                                   MealType = timeSlot.MealType,
                                   ReservationDay = timeSlot.ReservationDay
                               }).FirstOrDefaultAsync();

            return query;
        }

        public Task<User?> GetUserAsync(string emailId)
        {
            return _dbContext.Users.FirstOrDefaultAsync(f => f.Email.Equals(emailId));
        }
    }
}
