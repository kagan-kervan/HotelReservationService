using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        HotelService hotelService;
        public HotelController(HotelService hotelService)
        {
            this.hotelService = hotelService;
        }
        [HttpPost("add-hotel/{owner_id}/{address_id}")]
        public IActionResult AddHotel([FromBody]HotelVM hotelVM,int owner_id,int address_id)
        {
            /*
            if(!hotelService.HasOwnerWithGivenID(owner_id))
            {
                return BadRequest();
            }
            if(!hotelService.HasAddressWithGivenID(address_id))
                return BadRequest();
            */
            hotelService.AddHotel(hotelVM,owner_id,address_id);
            return(Ok());
        }
        [HttpGet("get-all-hotels")]
        public IActionResult GetAllHotels()
        {
            var hotels = hotelService.GetAllHotels();
            return Ok(hotels);
        }
        [HttpGet("get-hotel-id/{id}")]
        public IActionResult GetHotelFromID(int id) 
        {
            var hotel = hotelService.GetHotel(id);
            return Ok(hotel);
        }
        [HttpGet("get-hotel-with-owner-id/{owner_id}")]
        public IActionResult GetHotelsFromOwnerID(int owner_id)
        {
            /*
            if (!hotelService.HasOwnerWithGivenID(owner_id))
                return BadRequest();
            */
            var hotels = hotelService.GetHotelsFromOwnerID(owner_id);
            return Ok(hotels);
        }
        [HttpDelete("delete-hotel/{hotel_id}")]
        public IActionResult DeleteHotelFromID(int id)
        {
            hotelService.DeleteHotelWithID(id);
            return(Ok());
        }
        [HttpDelete("delete-owner-hotel/{owner_id}")]
        public IActionResult DeleteHotelsFromOwnerID(int owner_id)
        {
            /*
            if (!hotelService.HasOwnerWithGivenID(owner_id))
                return BadRequest();
            */
            hotelService.DeleteHotelWithOwnerID(owner_id);
            return(Ok());
        }
        [HttpPut("Update-hotel/{id}")]
        public IActionResult UpdateHotel(int id, [FromBody]HotelVM updatedHotel)
        {
            var hotel =hotelService.UpdateHotelWıthID(id,updatedHotel);
            return (Ok(hotel));
        }
        [HttpPut("update-hotel-address/{id}/{address_id}")]
        public IActionResult UpdateHotelAddress(int id, int address_id)
        {
            /*
           if(!hotelService.HasAddressWithGivenID(address_id))
                return BadRequest();
            */
           var hotel = hotelService.UpdateHotelAddress(id,address_id);
            return (Ok(hotel));
        }
        [HttpPut("update-hotel-owner/{id}/{owner_id}")]
        public IActionResult UpdateHotelOwner(int id, int owner_id)
        {
            /*
            if (!hotelService.HasOwnerWithGivenID(owner_id))
                return BadRequest();
            */
            var hotel = hotelService.UpdateHotelOwner(id,owner_id);
            return (Ok(hotel));
        }
    }
}
