using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Service.IServices;

namespace RestaurantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;   
        }

        [HttpGet("restaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        [HttpGet("branches/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _restaurantService.GetAllRestaurantBranchesByRestaurantIdAsync(restaurantId);
            if (branches == null)
            {
                return NotFound(); //404
            }
            return Ok(branches);
        }

        [HttpGet("diningTables/{branchId}")]
        public async Task<IActionResult> GetGetDiningTablesByBranchAsync(int branchId)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsunc(branchId);
            if (diningTables == null)
            {
                return NotFound(); //404
            }
            return Ok(diningTables);
        }

        [HttpGet("diningTables/{branchId}/{date}")]
        public async Task<IActionResult> GetGetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsunc(branchId, date);
            if (diningTables == null)
            {
                return NotFound(); //404
            }
            return Ok(diningTables);
        }
    }
}
