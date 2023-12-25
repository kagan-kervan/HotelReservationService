using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Services
{
    public class RoomService
    {
        private AppDBContext dbContext;
        private readonly TableRelationService tableRelationService;
        public RoomService(AppDBContext dbContext, TableRelationService tableRelationService)
        {
            this.dbContext = dbContext;
            this.tableRelationService = tableRelationService;
        }
        public void AddRoom(int hotel_id, int roomtype_id, RoomVM room)
        {
            var tempRoom = new Room()
            {
                HotelId = hotel_id,
                RoomTypeId = roomtype_id,
                Room_Number = room.Room_Number,
                isAvailable = room.isAvailable,
            };
            dbContext.Rooms.Add(tempRoom);
            dbContext.SaveChanges();
        }
        public ICollection<Room> GetRooms()
        {
            var list = dbContext.Rooms.Include(h => h.Hotel).Include(r => r.RoomType).ToList();
            return list;
        }
        public Room GetRoom(int id) 
        { 
            return dbContext.Rooms.Include(h => h.Hotel).Include(r => r.RoomType).SingleOrDefault(r => r.Id == id);
        }
        public ICollection<Room> GetRoomsFromHotelID(int hotel_id)
        {
            var list = dbContext.Rooms.Where(x => x.HotelId == hotel_id).Include(h => h.Hotel).Include(r => r.RoomType).ToList();
            return list;
        }
        public ICollection<Room> GetRoomsFromTypeID(int type_id)
        {
            var list = dbContext.Rooms.Where(x => x.RoomTypeId == type_id).Include(h => h.Hotel).Include(r => r.RoomType).ToList();
            return list;
        }
        public void RemoveRoom(int id)
        {
            var room = dbContext.Rooms.Find(id);
            //If there are any reservations attached to room.
            if (tableRelationService.DoesRoomHaveAnyReservations(id))
            {
                //Delete reservations.
                tableRelationService.DeleteReservationsAttachedToRoom(id);
            }
            dbContext.Rooms.Remove(room);
            dbContext.SaveChanges();
        }
        public void RemoveRoomsWithHotelID(int hoetl_id)
        {
            var list = dbContext.Rooms.Where(x => x.HotelId==hoetl_id).ToList();
            foreach (var room in list)
            {
                RemoveRoom(room.Id);
            }
            dbContext.SaveChanges();
        }
        public void UpdateRoom(int room_id,RoomVM room)
        {
            var temp = dbContext.Rooms.Find(room_id);
            if(temp != null)
            {
                temp.Room_Number = room.Room_Number;
                temp.isAvailable = room.isAvailable;
            }
            dbContext.SaveChanges();
        }
        public void UpdateRoomHotelID(int room_id,int hotel_id)
        {
            var temp = dbContext.Rooms.Find(room_id);
            if(temp != null)
            {
                temp.HotelId = hotel_id;
            }
            dbContext.SaveChanges();
        }
        public void UpdateRoomTypeID(int room_id,int type_id)
        {
            var temp = dbContext.Rooms.Find(room_id);
            if(temp != null)
            {
                temp.RoomTypeId = type_id;
            }
            dbContext.SaveChanges();
        }
    }
}
