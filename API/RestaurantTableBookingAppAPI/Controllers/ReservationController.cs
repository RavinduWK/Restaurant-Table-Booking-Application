using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Core;
using RestaurantTableBookingApp.Service.IServices;
using System.Security.Claims;

namespace RestaurantTableBookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }


        [HttpGet("{id}", Name = "GetReservation")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            // Your logic to retrieve and return a reservation
            return Ok();
        }


        [HttpPost("CheckIn")]
        [AllowAnonymous]
        public async Task<ActionResult<ReservationModel>> CheckInReservationAsync(DiningTableWithTimeSlotsModel reservation)
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
            var response = await reservationService.CheckInReservationAsync(reservation);
            return Ok(response);
        }


       
    }
}
