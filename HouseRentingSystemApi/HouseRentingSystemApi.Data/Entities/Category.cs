using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystemApi.Data.DataConstants.DataConstants.Category;


namespace HouseRentingSystemApi.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<House> Houses { get; set; } = new List<House>();
    }
}
