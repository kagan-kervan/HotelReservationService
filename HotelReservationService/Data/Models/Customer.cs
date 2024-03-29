﻿using HotelReservationService.Data.Identity;

namespace HotelReservationService.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email_Address { get; set; }
        public string Password { get; set;}
        public string Phone { get; set; }
        public ApplicationCustomer ApplicationCustomer { get; set; }
    }
}
