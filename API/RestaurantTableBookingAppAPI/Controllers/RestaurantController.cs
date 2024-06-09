using Microsoft.AspNetCore.Authorization;
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
            var diningTables = await _restaurantService.GetDiningTablesByBranchAsync(branchId);
            if (diningTables == null)
            {
                return NotFound(); //404
            }
            return Ok(diningTables);
        }

        [HttpGet("diningTables/{branchId}/{date}")]
        public async Task<IActionResult> GetGetDiningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {
            var diningTables = await _restaurantService.GetDiningTablesByBranchAndDateAsync(branchId, date);
            if (diningTables == null)
            {
                return NotFound(); //404
            }
            return Ok(diningTables);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> CreateReservationAsync(ReservationModel reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the selected time slot exists
            var timeSlot = await reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
            if (!timeSlot)
            {
                return NotFound("Selected time slot not found.");
            }

            // Create a new reservation
            var newReservation = new ReservationModel
            {
                UserId = reservation.UserId,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                EmailId = reservation.EmailId,
                PhoneNumber = reservation.PhoneNumber,
                TimeSlotId = reservation.TimeSlotId,
                ReservationDate = reservation.ReservationDate,
                ReservationStatus = reservation.ReservationStatus
            };

            var createdReservation = await reservationService.CreateOrUpdateReservationAsync(newReservation);
            await emailNotification.SendBookingEmailAsync(reservation);

            return new CreatedResult("GetReservation", new { id = createdReservation });
        }

        [HttpGet("getreservations")]
        public async Task<ActionResult> GetReservationDetails(int branchId, DateTime date)
        {
            var reservations = await reservationService.GetReservationDetails();

            return Ok(reservations);
        }
    }
}
