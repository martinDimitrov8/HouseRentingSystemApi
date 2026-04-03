using HouseRentingSystemApi.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

using static HouseRentingSystemApi.Data.DataConstants.DataConstants.House;

namespace HouseRentingSystemApi.Models
{
    public class HouseDetailModel
    {
        [BindNever]
    
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        public string Address { get; set; }

        public string ImageUrl { get; set; } 

        public string Description { get; set; }

        public decimal PricePerMonth { get; set; }

       

        public CategoryViewEnum Category { get; set; }


    }
}
