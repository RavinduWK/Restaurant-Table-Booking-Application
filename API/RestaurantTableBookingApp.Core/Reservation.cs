using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RestaurantTableBookingApp.Core;

public partial class Reservation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int TimeSlotId { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }

    [Required]
    public string ReservationStatus { get; set; } = null!;

    public bool? ReminderSent { get; set; }

    public virtual TimeSlot TimeSlot { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
