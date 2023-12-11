namespace HotelReservationService.Data.ViewModels
{
    public class HotelVM
    {

        public string HotelName { get; set; }
        public int total_room_number { get; set; }
        public int? full_room_number { get; set; }
        public float? overall_score { get; set; }
    }
}
