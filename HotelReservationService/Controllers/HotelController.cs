using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using HotelReservationService.Services.Params;
using Microsoft.AspNetCore.Authorization;
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
            var htl = hotelService.AddHotel(hotelVM,owner_id,address_id);
            if(htl != null)
                return(Ok(htl));
            return BadRequest("Failed to create hotel.");
        }
        [HttpGet("get-hotels")]
        public IActionResult GetAllHotels(int? ownerIDFilter,int? addressIDFilter, string? sortingType, string? searchWord, int pageIndex, int? pageSize)
        {
            HotelControllerParameters parameters = new HotelControllerParameters(ownerIDFilter, addressIDFilter, sortingType,searchWord,pageIndex, pageSize);
            var hotels = hotelService.GetAllHotels(parameters);
            if(hotels.Any())
                return Ok(hotels);
            return NotFound();
        }
        [HttpGet("get-hotel-id/{id}")]
        public IActionResult GetHotelFromID(int id) 
        {
            var hotel = hotelService.GetHotel(id);
            if(hotel != null)
                return Ok(hotel);
            return NotFound();
        }
        [HttpGet("get-hotel-with-owner-id/{owner_id}")]
        public IActionResult GetHotelsFromOwnerID(int owner_id)
        {
            /*
            if (!hotelService.HasOwnerWithGivenID(owner_id))
                return BadRequest();
            */
            var hotels = hotelService.GetHotelsFromOwnerID(owner_id);
            if(hotels != null)
                return Ok(hotels);
            return NotFound() ;
        }
        [HttpDelete("delete-hotel/{id}")]
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
