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
            if(list.Any()) 
                return Ok(list);
            return NotFound();
        }
        [HttpGet("get/{id}")]
        public IActionResult GetRoom(int id)
        {
            var room = roomService.GetRoom(id);
            if(room != null)
                return Ok(room);
            return NotFound();
        }
        [HttpGet("get-with-hotelID/{hotel_id}")]
        public IActionResult GetRoomsWithHotelID(int hotel_id,DateTime? checkin, DateTime? checkout) 
        {
            if(checkin != null && checkin < DateTime.Now)
            {
                return BadRequest("Check in time is lower than today.");
            }
            var rooms = roomService.GetReservedRoomsFromHotelID(hotel_id,checkin,checkout);
            if (!rooms.Any())
            {
                return NoContent();
            }
            return Ok(rooms);
        }
        [HttpGet("get-with-typeID/{type_id}")]
        public IActionResult GetRoomsWithTypeID(int type_id)
        {
            var rooms = roomService.GetRoomsFromTypeID(type_id);
            if(rooms.Any())
                return Ok(rooms);
            return NotFound();
        }
        [HttpPost("add/{hotel_id}/{roomtype_id}")]
        public IActionResult Post(int hotel_id,int roomtype_id, [FromBody]RoomVM roomVM) 
        {
            var room = roomService.AddRoom(hotel_id, roomtype_id,roomVM);
            return Ok(room);
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
