using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Core
{
    public class Chef
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Speciality { get; set; }
        public string ImageUrl { get; set; }
        public int RestaurantId { get; set; }  // Foreign key to Restaurant

        public Restaurant Restaurant { get; set; }
    }
}
