using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;

namespace HouseRentingSystemApi.Data.Entities
{
    public class House
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } 
        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }  

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public decimal PricePerMonth { get; set; }

        public Category Category { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; } 

        public ApplicationUser Owner { get; set; }
        [ForeignKey(nameof(Owner))]

        public string? UserId { get; set; }

    }
}
