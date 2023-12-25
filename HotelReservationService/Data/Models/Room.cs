namespace HotelReservationService.Data.Models
{
    public class Room
    {
        // Foreign Key
        public int? HotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }
        //Foreign Key
        public int? RoomTypeId { get; set; }
        public virtual RoomType? RoomType { get; set; }
        public int Id { get; set; }
        public int Room_Number { get; set; }
        public bool isAvailable { get; set; }
    }
}
