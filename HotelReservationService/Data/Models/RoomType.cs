﻿namespace HotelReservationService.Data.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Capacity { get; set; }

    }
}
