using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantTableBookingApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantTableBookingApp.Data.EntityTypeConfigurations
{
    public class ReviewEntityTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasOne(d => d.Restaurant)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(d => d.RestaurantId)
                   .HasConstraintName("FK_dbo_Reviews_dbo_RestaurantId");
        }
    }
}
