using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using HotelReservationService.Services.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private RoomService roomService;
        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpGet("get-rooms")]
        public IActionResult GetRooms(String? sortingType,int?hotelIDfilter,int?typeIDfilter,int pageIndex,int? PageSize)
        {
            RoomControllerParameters controllerParameters = new RoomControllerParameters(sortingType,hotelIDfilter,typeIDfilter,pageIndex,PageSize);
            var list = roomService.GetRooms(controllerParameters);
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = roomService.GetRoom(id);
            return Ok(room);
        }
        [HttpGet("get-with-hotelID/{hotel_id}")]
        public IActionResult GetRoomsWithHotelID(int hotel_id) 
        {
            var rooms = roomService.GetRoomsFromHotelID(hotel_id);
            return Ok(rooms);
        }
        [HttpGet("get-with-typeID/{type_id}")]
        public IActionResult GetRoomsWithTypeID(int type_id)
        {
            var rooms = roomService.GetRoomsFromTypeID(type_id);
            return Ok(rooms);
        }
        [HttpPost("add/{hotel_id}/{roomtype_id}")]
        public IActionResult Post(int hotel_id,int roomtype_id, [FromBody]RoomVM roomVM) 
        {
            roomService.AddRoom(hotel_id, roomtype_id,roomVM);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        public IActionResult Remove(int id)
        {
            roomService.RemoveRoom(id);
            return Ok();
        }
        [HttpDelete("remove-with-hotelID/{hotel_id}")]
        public IActionResult DeleteRoomsWithHotelID(int hotel_id)
        {
            roomService.RemoveRoomsWithHotelID(hotel_id) ;
            return Ok();
        }
        [HttpPut("update-room/{id}")]
        public IActionResult UpdateRoom(int id, [FromBody]RoomVM roomVM)
        {
            roomService.UpdateRoom(id,roomVM) ;
            return Ok();
        }
        [HttpPut("update-room-hotelId/{id}/{hotel_id}")]
        public IActionResult UpdateRoomHtoelID(int id, int hotel_id)
        {
            roomService.UpdateRoomHotelID(id, hotel_id);
            return Ok();
        }
        [HttpPut("update-room-tyepId/{id}/{type_id}")]
        public IActionResult UpdateRoomTypeID(int id, int type_id)
        {
            roomService.UpdateRoomTypeID(id, type_id) ;
            return Ok();
        }
    }
}
