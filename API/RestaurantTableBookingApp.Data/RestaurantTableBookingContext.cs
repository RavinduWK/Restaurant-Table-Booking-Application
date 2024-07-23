using RestaurantTableBookingApp.Core;
using Microsoft.EntityFrameworkCore;
using RestaurantTableBookingApp.Data.EntityTypeConfigurations;

namespace RestaurantTableBookingApp.Data;

public partial class RestaurantTableBookingDbContext : DbContext
{
    
    public RestaurantTableBookingDbContext(DbContextOptions<RestaurantTableBookingDbContext> options): base(options)
    { }

    public virtual DbSet<DiningTable> DiningTables { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Restaurant> Restaurants { get; set; }
    public virtual DbSet<RestaurantBranch> RestaurantBranches { get; set; }
    public virtual DbSet<TimeSlot> TimeSlots { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<Chef> Chefs { get; set; }
    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfiguration(new MenuEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChefEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EventEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewEntityTypeConfiguration());
    }

}
