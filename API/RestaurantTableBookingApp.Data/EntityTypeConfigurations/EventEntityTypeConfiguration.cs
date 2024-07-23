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
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasOne(d => d.Restaurant)
                   .WithMany(p => p.Events)
                   .HasForeignKey(d => d.RestaurantId)
                   .HasConstraintName("FK_dbo_Events_dbo_RestaurantId");
        }
    }
}
