using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HotelReservationService.Services.AddressService;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressService AddressService;
        public AddressController(AddressService addressService)
        {
            AddressService = addressService;
        }
        [HttpPost("add-address")]
        public IActionResult AddAddress([FromBody] AddressVM AddressVM)
        {
            AddressService.AddAddress(AddressVM);
            return Ok();
        }
        [HttpGet("get-all")]
        public IActionResult GetAddresses() 
        {
            var addresses = AddressService.GetAddresses();
            return Ok(addresses);
        }
        [HttpGet("get-address/{id}")]
        public IActionResult GetAddressGivenID(int id)
        { 
            var address = AddressService.GetAddress(id);
            return Ok(address);
        }
        [HttpDelete("delete-address/{id}")]
        public IActionResult DeleteAddress(int id)
        {
            
             if(AddressService.IsAddressHasHotel(id))
            {
                return BadRequest();
            }
            AddressService.RemoveAddress(id);
            return Ok();
        }
        [HttpPut("update-address/{id}")]
        public IActionResult UpdateAddress(int id, [FromBody]AddressVM newAddress)
        {
            var address = AddressService.UpdateAddressGivenID(id, newAddress);
            return Ok(address);
        }
    }
}
