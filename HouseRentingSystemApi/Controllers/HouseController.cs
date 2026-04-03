using HouseRentingSystemApi.Data;
using HouseRentingSystemApi.Data.Entities;
using HouseRentingSystemApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HouseRentingSystemApi.Controllers
{
    [Route("api/[controller]")]
    public class HouseController : ControllerBase
    {
        private AppDbContext context;

        public HouseController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("All")]
        [Produces(typeof(IEnumerable<HouseDetailModel>))]
        public async Task<IActionResult> GetAll()
        {
            var model = await context.Houses
                .AsNoTracking()
                .Select(h => new HouseDetailModel()
                {

                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl
                })
                .ToListAsync();

            return Ok(model);
        }

        [HttpGet("{id}")]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> GetById(int id)
        {
            var house = await context.Houses.FirstOrDefaultAsync(h => h.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return Ok(new HouseDetailModel()
            {
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl
            });
        }
        [Authorize]
        [HttpPost]
        [Produces(typeof(HouseDetailModel))]
        public async Task<IActionResult> Create([FromBody] HouseDetailModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var newHouse = new House()
            {
                Description = model.Description,
                PricePerMonth = model.PricePerMonth,
                Address = model.Address,
                Title = model.Title,
                ImageUrl = model.ImageUrl
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Name ==  model.Category
                .ToString());
            if(category == null)
            {
                var newCategory = new Category()
                {
                    Name = model.Category.ToString(),
                };
                context.Categories.Add(newCategory);
                await context.SaveChangesAsync();
                newHouse.CategoryId = newCategory.Id; 
                
            }
            else
            {
                newHouse.CategoryId = category.Id;
            }
            context.Houses.Add(newHouse);
            await context.SaveChangesAsync();
            return Created($"api/{newHouse.Id}", new HouseDetailModel()
                {
                    Address = newHouse.Address,
                    ImageUrl = newHouse.ImageUrl,
                    Title = newHouse.Title,
                    Description = newHouse.Description,
                    PricePerMonth = newHouse.PricePerMonth,
                    Category = model.Category
            });
        }
    }
}