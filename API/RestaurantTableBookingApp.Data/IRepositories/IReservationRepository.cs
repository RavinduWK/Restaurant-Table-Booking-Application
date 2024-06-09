using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Data.IRepositories
{
    public interface IReservationRepository
    {
        Task<int> CreateOrUpdateReservationAsync(ReservationModel reservation);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);
        Task<DiningTableWithTimeSlotsModel> UpdateReservationAsync(DiningTableWithTimeSlotsModel reservation);
        Task<List<ReservationDetailsModel>> GetReservationDetailsAsync();
    }
}
