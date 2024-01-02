using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;

namespace HotelReservationService.Services
{
    public class RoomTypeService
    {
        private AppDBContext appDBContext;
        public RoomTypeService(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public RoomType AddRoomType(RoomTypeVM roomTypeVM)
        {
            var roomtype = new RoomType()
            {
                TypeName = roomTypeVM.TypeName,
                Description = roomTypeVM.Description,
                Price = roomTypeVM.Price,
                Capacity = roomTypeVM.Capacity,
            };
            appDBContext.RoomTypes.Add(roomtype);
            appDBContext.SaveChanges();
            return roomtype;
        }
        public ICollection<RoomType> GetRoomTypes() 
        { 
            var list = appDBContext.RoomTypes.ToList();
            return list;
        }

        public RoomType GetRoomTypeWithID(int id)
        {
            var type = appDBContext.RoomTypes.Find(id);
            return type;
        }

        public void RemoveRoomType(int id)
        {
            var type = appDBContext.RoomTypes.Find(id);
            appDBContext.RoomTypes.Remove(type);
            appDBContext.SaveChanges();
        }

        public void UpdateRoomType(int id,RoomTypeVM roomTypeVM)
        {
            var type = appDBContext.RoomTypes.Find(id);
            if(type != null)
            {
                type.TypeName = roomTypeVM.TypeName;
                type.Description = roomTypeVM.Description;
                type.Price = roomTypeVM.Price;
                type.Capacity = roomTypeVM.Capacity;

            }
            appDBContext.SaveChanges(true);
        }

    }
}
