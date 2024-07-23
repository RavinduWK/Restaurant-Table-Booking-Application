using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Core
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }  // Foreign key to Restaurant

        public Restaurant Restaurant { get; set; } 
    }

}
