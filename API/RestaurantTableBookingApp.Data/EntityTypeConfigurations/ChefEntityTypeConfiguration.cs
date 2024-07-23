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
    public class ChefEntityTypeConfiguration : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.ToTable("Chefs");

            builder.HasOne(d => d.Restaurant)
                   .WithMany(p => p.Chefs)
                   .HasForeignKey(d => d.RestaurantId)
                   .HasConstraintName("FK_dbo_Chefs_dbo_RestaurantId");
        }
    }
}
