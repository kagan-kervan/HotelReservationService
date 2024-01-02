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
            if(list.Any()) 
                return Ok(list);
            return NotFound();
        }
        [HttpGet("get/{id}")]
        public IActionResult GetOwner(int id) 
        { 
            var owner = ownerService.GetOwner(id);
            if(owner != null)
                return Ok(owner);
            return NotFound();
        }
        [HttpGet("get-with-mail")]
        public IActionResult GetOwnerMail(string email)
        {
            var owner = ownerService.GetOwnerWithMail(email);
            if(owner != null)
                return Ok(owner);
            return NotFound();
        }
        [HttpPost("add")]
        public IActionResult AddOwner([FromBody] OwnerVM ownerVM)
        {
            var own = ownerService.AddOwner(ownerVM);
            return Ok(own);
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateOwner(int id, [FromBody] OwnerVM ownerVM)
        {
            var own = ownerService.UpdateOwner(id, ownerVM);
            return Ok(own);
        }
        [HttpDelete("remove/{id}")]
        public IActionResult DeleteOwner(int id)
        {
            ownerService.DeleteOwner(id);
            return Ok();
        }
    }
}
