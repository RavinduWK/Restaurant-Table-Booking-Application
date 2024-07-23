using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Core
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }  // A rating out of 5
        public DateTime Date { get; set; }
        public int RestaurantId { get; set; }  // Foreign key to Restaurant

        public Restaurant Restaurant { get; set; }
    }

}
