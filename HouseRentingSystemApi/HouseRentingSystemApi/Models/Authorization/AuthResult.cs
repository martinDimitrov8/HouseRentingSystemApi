using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace HouseRentingSystemApi.Models.Authorization
{
    public class AuthResult
    {
        public int Code { get; set; }
        public string Massage { get; set; }

        public string Token { get; set; }
    }
}
