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
    internal class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.Property(h => h.PricePerMonth)
                   .HasPrecision(18, 2);

            builder.HasData(SeedHouses());
        }

        private IEnumerable<House> SeedHouses()
        {
            return new List<House>
        {
            new House
            {
                Id = 11,
                Title = "Modern Apartment in Sofia Center",
                Address = "ul. Vitosha 15, Sofia",
                Description = "Spacious modern apartment near city center with great view.",
                ImageUrl = "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688",
                PricePerMonth = 850,
                CategoryId = 1,
                UserId = null
            },
            new House
            {
                Id = 12,
                Title = "Cozy Studio in Studentski Grad",
                Address = "Studentski Grad 45, Sofia",
                Description = "Perfect for students, fully furnished studio.",
                ImageUrl = "https://images.unsplash.com/photo-1493809842364-78817add7ffb",
                PricePerMonth = 450,
                CategoryId = 2,
                UserId = null
            },
            new House
            {
                Id = 13,
                Title = "Luxury Penthouse",
                Address = "Lozenets, Sofia",
                Description = "High-end penthouse with terrace and parking.",
                ImageUrl = "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2",
                PricePerMonth = 2000,
                CategoryId = 1,
                UserId = null
            },
            new House
            {
                Id = 14,
                Title = "Small House in Village",
                Address = "Bistritsa Village",
                Description = "Quiet place with yard and nature around.",
                ImageUrl = "https://images.unsplash.com/photo-1572120360610-d971b9d7767c",
                PricePerMonth = 300,
                CategoryId = 3,
                UserId = null
            },
            new House
            {
                Id = 15,
                Title = "Family House with Garden",
                Address = "Dragalevtsi, Sofia",
                Description = "Big house suitable for family with garden.",
                ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                PricePerMonth = 1200,
                CategoryId = 3,
                UserId = null
            },
            new House
            {
                Id = 16,
                Title = "One Bedroom Apartment",
                Address = "Mladost 2, Sofia",
                Description = "Comfortable one-bedroom apartment.",
                ImageUrl = "https://images.unsplash.com/photo-1507089947368-19c1da9775ae",
                PricePerMonth = 600,
                CategoryId = 1,
                UserId = null
            },
            new House
            {
                Id = 17,
                Title = "Cheap Room for Rent",
                Address = "Nadezhda, Sofia",
                Description = "Budget room, ideal for short stay.",
                ImageUrl = "https://images.unsplash.com/photo-1554995207-c18c203602cb",
                PricePerMonth = 200,
                CategoryId = 2,
                UserId = null
            },
            new House
            {
                Id = 18,
                Title = "Sea View Apartment",
                Address = "Varna Center",
                Description = "Beautiful apartment with sea view.",
                ImageUrl = "https://images.unsplash.com/photo-1494526585095-c41746248156",
                PricePerMonth = 900,
                CategoryId = 1,
                UserId = null
            },
            new House
            {
                Id = 19,
                Title = "Mountain Cabin",
                Address = "Borovets",
                Description = "Wooden cabin perfect for winter getaway.",
                ImageUrl = "https://images.unsplash.com/photo-1449844908441-8829872d2607",
                PricePerMonth = 700,
                CategoryId = 3,
                UserId = null
            },
            new House
            {
                Id = 20,
                Title = "Minimalist Apartment",
                Address = "Plovdiv Center",
                Description = "Clean and minimalist design, great location.",
                ImageUrl = "https://images.unsplash.com/photo-1499951360447-b19be8fe80f5",
                PricePerMonth = 650,
                CategoryId = 1,
                UserId = null
            }
        };
        }
    }
}
