using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;

namespace HotelReservationService.Services
{
    public class HotelService
    {
        AppDBContext dbContext;
        public HotelService(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddHotel(HotelVM hotelVM,int owner_id,int address_id)
        {
            var newHotel = new Hotel()
            {
                HotelOwnerId = owner_id,
                HotelAddressId = address_id,
                HotelName = hotelVM.HotelName,
                total_room_number = hotelVM.total_room_number,
                full_room_number = hotelVM.full_room_number
            };
            dbContext.Hotels.Add(newHotel);
            dbContext.SaveChanges();
        }
        public ICollection<Hotel> GetAllHotels() 
        { 
            var allHotels = dbContext.Hotels.ToList();
            return allHotels;
        }
        public Hotel GetHotel(int id) {  return dbContext.Hotels.Find(id); }
        public ICollection<Hotel> GetHotelsFromOwnerID(int owner_id)
        {
            var hotels = dbContext.Hotels.Where(s => s.HotelOwnerId == owner_id).ToList();
            return hotels;
        }
        public void DeleteHotelWithID(int id)
        {
            var hotel = dbContext.Hotels.Find(id);
            if (hotel != null)
            {
                dbContext.Hotels.Remove(hotel);
                dbContext.SaveChanges();
            }
        }
        public void DeleteHotelWithOwnerID(int owner_id)
        {
            var hotels = GetHotelsFromOwnerID(owner_id);
            foreach (var hotel in hotels)
            {
                dbContext.Remove(hotel);
            }
            dbContext.SaveChanges();
        }
        public Hotel UpdateHotelWıthID(int id, HotelVM hotelModel)
        {
            var hotel = dbContext.Hotels.Find(id);
            if (hotel != null)
            {
                hotel.HotelName = hotelModel.HotelName;
                hotel.total_room_number=hotelModel.total_room_number;
                hotel.full_room_number = hotelModel.full_room_number;
                hotel.overall_score = hotelModel.overall_score;
            }
            dbContext.SaveChanges() ;
            return hotel;
        }

        public bool HasOwnerWithGivenID(int owner_id)
        {
            var tempOwner = dbContext.Owners.Find(owner_id);
            if(tempOwner != null)
            {
                return true;
            }
            return false;
        }
        public bool HasAddressWithGivenID(int address_id) 
        {
            var tempAddress = dbContext.Addresses.Find(address_id);
            if(tempAddress != null)
                return true;
            return false;
        }
    }
}
