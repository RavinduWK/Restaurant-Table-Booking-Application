using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Service.IServices;
using RestaurantTableBookingApp.Service.Services;

namespace RestaurantTableBookingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] //The end points in this controller should be used without logging in
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IReservationService _reservationService;
        private readonly IEmailNotificationService _emailNotification;

        public RestaurantController(IRestaurantService restaurantService, IReservationService reservationService, IEmailNotificationService emailNotification)
        {
            _restaurantService = restaurantService;
            _reservationService = reservationService;
            _emailNotification = emailNotification;
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if the selected time slot exists
                var timeSlot = await _reservationService.TimeSlotIdExistAsync(reservation.TimeSlotId);
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

                var createdReservation = await _reservationService.CreateOrUpdateReservationAsync(newReservation);
                await _emailNotification.SendBookingEmailAsync(reservation);

                return new CreatedResult("GetReservation", new { id = createdReservation });
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }           
        }

        [HttpGet("getreservations")]
        public async Task<ActionResult> GetReservationDetails(int branchId, DateTime date)
        {
            var reservations = await _reservationService.GetReservationDetails();

            return Ok(reservations);
        }
    }
}
