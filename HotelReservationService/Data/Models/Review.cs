namespace HotelReservationService.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime Comment_Date { get; set; }

    }
}
