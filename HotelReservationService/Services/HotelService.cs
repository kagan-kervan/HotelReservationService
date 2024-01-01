using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        public Hotel AddHotel(HotelVM hotelVM,int owner_id,int address_id)
        {
            var newHotel = new Hotel()
            {
                HotelOwnerId = owner_id,
                HotelAddressId = address_id,
                HotelName = hotelVM.HotelName,
                total_room_number = hotelVM.total_room_number,
                full_room_number = (int?)hotelVM.full_room_number
            };
            dbContext.Hotels.Add(newHotel);
            dbContext.SaveChanges();
            return newHotel;
        }
        public ICollection<Hotel> GetAllHotels(Params.HotelControllerParameters parameters) 
        {
            IQueryable<Hotel> hotelQuery = dbContext.Hotels;
            //Filtering
            if(parameters.ownerIDFilter != null)
            {
                hotelQuery = hotelQuery.Where(x => x.HotelOwnerId == parameters.ownerIDFilter);
            }
            if(parameters.addressIDFilter != null)
            {
                hotelQuery = hotelQuery.Where(x => x.HotelAddressId == parameters.addressIDFilter);
            }
            //Searching
            if(parameters.searchWord != null) 
            {
                hotelQuery = hotelQuery.Where(x => x.HotelAddress.Country.Contains(parameters.searchWord) || 
                x.HotelAddress.City.Contains(parameters.searchWord) || x.HotelName.Contains(parameters.searchWord));
            }
            //Sorting
            switch(parameters.storingType)
            {
                case "nameAsc":
                    hotelQuery = hotelQuery.OrderBy(x => x.HotelName);
                    break;
                case "nameDesc":
                    hotelQuery = hotelQuery.OrderByDescending(x => x.HotelName);
                    break;
                case "reviewAsc":
                    hotelQuery = hotelQuery.OrderBy(x => x.overall_score);
                    break;
                case "reviewDesc":
                   hotelQuery = hotelQuery.OrderByDescending(x => x.overall_score);
                    break;
                default:
                    break;

            };
            hotelQuery = hotelQuery.Include(o  => o.HotelOwner).Include(a => a.HotelAddress);
            //Paging
            int totalPages = (int) Math.Ceiling(hotelQuery.Count() / (double)parameters.pageSize);
            parameters.pageIndex = Math.Min(totalPages, parameters.pageIndex);
            if (parameters.HasNextPage(totalPages))
            {
                hotelQuery = hotelQuery.Skip((parameters.pageIndex - 1) * parameters.pageSize).Take(parameters.pageSize);
            }
            var list = hotelQuery.ToList();
            return list;
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
