using RestaurantTableBookingApp.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Service.IServices
{
    public interface IReservationService
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<bool> TimeSlotIdExistAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotsModel> CheckInReservationAsync(DiningTableWithTimeSlotsModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetails();
    }
}
