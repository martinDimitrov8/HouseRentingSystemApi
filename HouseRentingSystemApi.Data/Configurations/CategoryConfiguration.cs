using HouseRentingSystemApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HouseRentingSystemApi.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new List<Category>()
        {
            new Category()
            {
                Id = 1,
                Name = "Apartment"
            },
            new Category()
            {
                Id = 2,
                Name = "Room"
            },
            new Category()
            {
                Id = 3,
                Name = "House"
            }
        });
        }
    }
}
