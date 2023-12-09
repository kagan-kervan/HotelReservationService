namespace HotelReservationService.Data.Models
{
    public class HotelFeatures
    {
        public int Id {  get; set; }
        // Foreign Key 
        public int HotelId { get; set; }
        // Navigation Property
        public virtual Hotel? Hotel { get; set; }
        public bool hasWifi { get; set; }
        public bool hasBeach {  get; set; }
        public bool hasSauna { get; set; }
        public bool hasSpa {  get; set; }
        public bool hasAquapark {  get; set; }
        public bool hasPool { get; set; }
        public bool hasBar {  get; set; }
        public bool hasParkingLot {  get; set; }
        public bool hasRoomService { get; set; }
        public bool hasRestaurant { get; set; }
        public bool hasBuffet { get; set; }
    }
}
