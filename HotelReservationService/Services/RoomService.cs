using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services.Params;
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
        public Room AddRoom(int hotel_id, int roomtype_id, RoomVM room)
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
            return tempRoom;
        }
        public ICollection<Room> GetRooms(RoomControllerParameters roomParams)
        {
            IQueryable<Room> queryable = dbContext.Rooms;
            if (roomParams.hotelIDFilter != null)
            {
                queryable = queryable.Where(x => x.HotelId == roomParams.hotelIDFilter);
            }
            if(roomParams.typeIDFilter != null)
            {
                queryable = queryable.Where(x => x.RoomTypeId == roomParams.typeIDFilter);
            }
            switch (roomParams.sortingType)
            {
                case "nameAsc":
                    queryable = queryable.OrderBy(x => x.RoomType.TypeName); 
                    break;
                case "nameDesc":
                    queryable = queryable.OrderByDescending(z => z.RoomType.TypeName);
                    break;
                case "priceAsc":
                    queryable = queryable.OrderBy(p => p.RoomType.Price); 
                    break;
                case "priceDesc":
                    queryable = queryable.OrderByDescending(p => p.RoomType.Price);
                    break;
                default:
                    queryable = queryable.OrderBy(x => x.Id); break;
            }
            queryable = queryable.Include(x => x.Hotel).Include(y => y.RoomType);
            int totalPages = (int)Math.Ceiling(queryable.Count() / (double)roomParams.pageSize);
            if (roomParams.HasNextPage(totalPages))
            {
                queryable = queryable.Skip((roomParams.pageIndex - 1) * roomParams.pageSize).Take(roomParams.pageSize);
            }
            return queryable.ToList();
        }
        public Room GetRoom(int id) 
        { 
            return dbContext.Rooms.Include(h => h.Hotel).Include(r => r.RoomType).SingleOrDefault(r => r.Id == id);
        }
        public ICollection<Room> GetRoomsFromHotelID(int hotel_id)
        {
            IQueryable<Room> query = dbContext.Rooms;
            query = query.Where(x => x.HotelId == hotel_id);
            query = query.Include(h => h.Hotel).Include(r => r.RoomType);
            return query.ToList();
        }public ICollection<Room> GetReservedRoomsFromHotelID(int hotel_id,DateTime? checkin,DateTime? checkout)
        {
            IQueryable<Room> query = dbContext.Rooms;
            query = query.Where(x => x.HotelId == hotel_id);
            if(checkin != null)
            {// Get rooms that are reserved during the specified time period
                var reservedRoomIds = dbContext.Reservations
                    .Where(x => (x.CheckInDate >= checkin && x.CheckInDate <= checkout) ||
                (x.CheckOutDate >= checkin && x.CheckOutDate <= checkout)).Select(y => y.RoomId).ToList();
                // Exclude reserved rooms from the main query
                query = query.Where(x => !reservedRoomIds.Contains(x.Id));
            }
            query = query.Include(h => h.Hotel).Include(r => r.RoomType);
            return query.ToList();
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
