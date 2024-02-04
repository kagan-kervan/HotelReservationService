using Microsoft.AspNetCore.Identity;

namespace HotelReservationService.Data.Identity
{
    public class ApplicationCustomer : IdentityUser
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }

    }
}
