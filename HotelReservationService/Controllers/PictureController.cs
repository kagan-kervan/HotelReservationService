using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private PictureService pictureService;
        public PictureController(PictureService pictureService)
        {
            this.pictureService = pictureService;
        }
        [HttpGet("get-pictures/{hotel_id}")]
        public IActionResult GetPictures(int hotel_id)
        {
            var list = pictureService.GetPictures(hotel_id);
            if(list.Any())
                return Ok(list);
            return NotFound();
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var rev = pictureService.GetPicture(id);
            if(rev != null)
                return Ok(rev);
            return NotFound();
        }
        [HttpPost("add/{hotel_id}")]
        public IActionResult AddPicture(string url, int hotel_id)
        {
            pictureService.AddPicture(url, hotel_id);
            return Ok();
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdatePicture(int id,string url)
        {
            pictureService.UpdatePicture(id, url);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(int id)
        {
            pictureService.DeletePicture(id);
            return Ok();
        }
    }
}
