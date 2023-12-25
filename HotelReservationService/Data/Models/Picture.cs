namespace HotelReservationService.Data.Models
{
    public class Picture
    {
        public int Id { get; set; }
        //Foreign Key
        public int? HotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public string Pic_Url { get; set; }
    }
}
