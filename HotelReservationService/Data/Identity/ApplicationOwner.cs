using Microsoft.AspNetCore.Identity;

namespace HotelReservationService.Data.Identity
{
    public class ApplicationOwner:IdentityUser
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
