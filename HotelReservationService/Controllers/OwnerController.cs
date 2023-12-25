using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private OwnerService ownerService;
        public OwnerController(OwnerService ownerService)
        {
            this.ownerService = ownerService;
        }
        [HttpGet("get-all")]
        public IActionResult GetAllOwners()
        {
            var list = ownerService.GetAll();
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetOwner(int id) 
        { 
            var owner = ownerService.GetOwner(id);
            return Ok(owner);
        }
        [HttpPost("add")]
        public IActionResult AddOwner([FromBody] OwnerVM ownerVM)
        {
            ownerService.AddOwner(ownerVM);
            return Ok();
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateOwner(int id, [FromBody] OwnerVM ownerVM)
        {
            ownerService.UpdateOwner(id, ownerVM);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        public IActionResult DeleteOwner(int id)
        {
            ownerService.DeleteOwner(id);
            return Ok();
        }
    }
}
