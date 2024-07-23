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

        [HttpGet("menu/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantMenuByRestaurantIdAsync(int restaurantId)
        {
            var menu = await _restaurantService.GetRestaurantMenuByRestaurantIdAsync(restaurantId);
            if (menu == null)
            {
                return NotFound(); //404
            }
            return Ok(menu);
        }

        [HttpGet("chefs/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantChefsByRestaurantIdAsync(int restaurantId)
        {
            var chefs = await _restaurantService.GetRestaurantChefsByRestaurantIdAsync(restaurantId);
            if (chefs == null)
            {
                return NotFound(); //404
            }
            return Ok(chefs);
        }


        [HttpGet("events/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantEventsByRestaurantIdAsync(int restaurantId)
        {
            var events = await _restaurantService.GetRestaurantEventsByRestaurantIdAsync(restaurantId);
            if (events == null)
            {
                return NotFound(); //404
            }
            return Ok(events);
        }

        [HttpGet("reviews/{restaurantId}")]
        public async Task<IActionResult> GetRestaurantReviewsByRestaurantIdAsync(int restaurantId)
        {
            var reviews = await _restaurantService.GetRestaurantReviewsByRestaurantIdAsync(restaurantId);
            if (reviews == null)
            {
                return NotFound(); //404
            }
            return Ok(reviews);
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
