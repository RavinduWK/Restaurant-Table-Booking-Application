using RestaurantTableBookingApp.Core.ViewModels;
using RestaurantTableBookingApp.Data.IRepositories;
using RestaurantTableBookingApp.Service.IServices;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RestaurantTableBookingApp.Service.IServices
{
    public interface IEmailNotificationService
    {
        Task<Response> SendBookingEmailAsync(ReservationModel model, bool isReminderEmail = false);
        Task<Response> SendCheckInEmailAsync(DiningTableWithTimeSlotsModel model);
    }

}