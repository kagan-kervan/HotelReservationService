namespace HotelReservationService.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        //Foreign Key
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        //Foreign Key
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        //Foreign Key
        public int ReviewId { get; set; }
        public virtual Review? Review { get; set; }
        public int Total_Guest_Number { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set;}
    }
}
