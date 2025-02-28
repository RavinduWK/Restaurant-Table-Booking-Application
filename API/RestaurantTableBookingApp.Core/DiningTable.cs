﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantTableBookingApp.Core;

public partial class DiningTable
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RestaurantBranchId { get; set; }

    [MaxLength(100)]
    public string? TableName { get; set; }

    [Required]
    public int Capacity { get; set; }

    public virtual RestaurantBranch RestaurantBranch { get; set; } = null!;

    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
