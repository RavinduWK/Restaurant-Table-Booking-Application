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
    public class MenuEntityTypeConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");

            builder.HasOne(d => d.Restaurant)
                   .WithMany(p => p.Menus)
                   .HasForeignKey(d => d.RestaurantId)
                   .HasConstraintName("FK_dbo_Menus_dbo_RestaurantId");
        }
    }
}
