using HouseRentingSystemApi.Models.Enums;

namespace HouseRentingSystemApi.Models
{
    public class HouseDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerMonth { get; set; }
        public CategoryViewEnum Category { get; set; }

        public bool IsRented { get; set; }
    }
}
