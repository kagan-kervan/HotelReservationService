using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Services
{
    public class HotelService
    {
        AppDBContext dbContext;
        private readonly TableRelationService tableRelationService;
        public HotelService(AppDBContext dbContext, TableRelationService tableRelation)
        {
            this.dbContext = dbContext;
            this.tableRelationService = tableRelation;
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
            var allHotels = dbContext.Hotels.Include(o => o.HotelOwner).Include(a => a.HotelAddress).ToList();
            return allHotels;
        }
        public Hotel GetHotel(int id) 
        {  
            return dbContext.Hotels.Include(o => o.HotelOwner).Include(a => a.HotelAddress).SingleOrDefault( x => x.Id == id); 
        }
        public ICollection<Hotel> GetHotelsFromOwnerID(int owner_id)
        {
            var hotels = dbContext.Hotels.Where(s => s.HotelOwnerId == owner_id).Include(o => o.HotelOwner).Include(a => a.HotelAddress).ToList();
            return hotels;
        }
        public void DeleteHotelWithID(int id)
        {
            var hotel = dbContext.Hotels.Find(id);
            // If the hotel has any rooms.
            if (tableRelationService.DoesHotelHaveAnyRooms(id))
            {
                tableRelationService.DeleteRoomsAttachedToHotel(id);
            }
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
        public Hotel UpdateHotelAddress(int id, int new_address_id)
        {
            var hotel = dbContext.Hotels.Find(id);
            if(hotel != null)
            {
                hotel.HotelAddressId = new_address_id;
                dbContext.SaveChanges();
            }
            return hotel;
        }
        public Hotel UpdateHotelOwner(int id, int new_owner_id)
        {
            var hotel = dbContext.Hotels.Find(id);
            if (hotel != null)
            {
                hotel.HotelOwnerId = new_owner_id;
                dbContext.SaveChanges();
            }
            return hotel;
        }
    }
}
