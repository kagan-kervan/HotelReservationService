namespace HotelReservationService.Data.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        // Foreign Key
        public int? HotelOwnerId { get; set;}
        // Navigation properties
        public virtual Owner? HotelOwner {  get; set;}
        //Foreign Key
        public int? HotelAddressId { get; set;}
        // Navigation properties
        public virtual Address? HotelAddress { get; set;}
        public string HotelName { get; set; }
        public int total_room_number { get; set;}
        public int? full_room_number { get; set;}
        public float? overall_score { get; set;}




    }
}
