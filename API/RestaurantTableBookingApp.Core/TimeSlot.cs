using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantTableBookingApp.Core;

public partial class TimeSlot
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DiningTableId { get; set; }

    [Required]
    public DateTime ReservationDay { get; set; }

    [Required]
    public string MealType { get; set; } = null!;

    [Required]
    public string TableStatus { get; set; } = null!;

    public virtual DiningTable DiningTable { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
