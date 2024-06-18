﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RestaurantTableBookingApp.Core;

public partial class Restaurant
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; } = null!;

    public string? OpenTime { get; set; }

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public virtual ICollection<RestaurantBranch> RestaurantBranches { get; set; } = new List<RestaurantBranch>();
}
