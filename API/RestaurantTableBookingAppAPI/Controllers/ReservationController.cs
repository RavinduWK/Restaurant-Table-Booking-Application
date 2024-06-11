using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Core;
using RestaurantTableBookingApp.Service.IServices;
using System.Security.Claims;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace RestaurantTableBookingApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private ClaimsPrincipal _currentPrincipal;
        private string _currentPrincipalId = string.Empty;
        private readonly IEmailNotificationService _emailNotificationservice;


        public ReservationController(IReservationService reservationService, IHttpContextAccessor contextAccessor, IEmailNotificationService emailNotificationService)
        {
            this.reservationService = reservationService;
            _contextAccessor = contextAccessor;
            _currentPrincipal = GetCurrentClaimsPrincipal();
            _emailNotificationservice = emailNotificationService;

            if (!IsAppOnlyToken() && _currentPrincipal != null)
            {
                _currentPrincipalId = _currentPrincipal.GetNameIdentifierId(); // use "sub" claim as a unique identifier in B2C

            }
        }


        [HttpGet("{id}", Name = "GetReservation")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            // Your logic to retrieve and return a reservation
            return Ok();
        }


        [HttpPost("CheckIn")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
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
            await _emailNotificationservice.SendCheckInEmailAsync(reservation);
            return Ok(response);
        }

        
        private ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
            // Irrespective of whether a user signs in or not, the AspNet security middleware dehydrates 
            // the claims in the HttpContext.User.Claims collection
            if (_contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User != null)
            {
                return _contextAccessor.HttpContext.User;
            }

            return null;
        }

 
        private bool IsAppOnlyToken()
        {
            // Add in the optional 'idtyp' claim to check if the access token is coming from an application or user.
            if (GetCurrentClaimsPrincipal() != null)
            {
                return GetCurrentClaimsPrincipal().Claims.Any(c => c.Type == "idtyp" && c.Value == "app");
            }

            return false;
        }



    }
}
