using HouseRentingSystemApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystemApi.Middelewere
{
    public class CustomMiddelewere
    {
        private readonly RequestDelegate next;
        public CustomMiddelewere(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext ctx)
        {
            var housesCount =await  ctx.Houses.CountAsync();
            Console.WriteLine($"Total houses in DB = {housesCount}");
            await this.next(context);
            var housesCounrNew = await ctx.Houses.CountAsync();
            if( housesCounrNew != housesCount)
            {
                Console.WriteLine($"There is {housesCounrNew - housesCount} already existed");

            }
            else
            {
                Console.WriteLine("Ther is no houses");
            }
        }
    }

}
