using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        public HotelFeaturesService hotelFeaturesService;
        public FeatureController(HotelFeaturesService hotelFeaturesService)
        {
            this.hotelFeaturesService = hotelFeaturesService;
        }
        [HttpPost("add-features/{hotel_id}")]
        public IActionResult AddFeature(int hotel_id, [FromBody]HotelFeaturesVM features)
        {
            /*
            if(!hotelFeaturesService.IsDBHasGivenHotelID(hotel_id))
            {
                return BadRequest();
            }
            */
            hotelFeaturesService.AddHotelFeatures(hotel_id, features);
            return Ok();
        }
        [HttpGet("Get-Feature/{id}")]
        public IActionResult GetFeature(int id) 
        { 
            var feature = hotelFeaturesService.GetFeaturesFromID(id);
            if(feature != null)
            {
                return Ok(feature);

            }
            return NotFound();
        }
        [HttpGet("Get-All-Features")]
        public IActionResult GetAllFeatures()
        {
            var features = hotelFeaturesService.GetHotelFeatures();
            if(features.Any())
                return Ok(features);
            return NotFound();
        }
        [HttpGet("Get-Feature-With-HotelID/{hotel_id}")]
        public IActionResult GetFeaturesFromHotelID(int hotel_id)
        {
            var feature = hotelFeaturesService.GetFeatureFromHotelID(hotel_id);
            if(feature != null)
                return Ok(feature);
            return NotFound();
        }
        [HttpPut("Update-Feature/{id}")]
        public IActionResult UpdateFeature(int id, [FromBody]HotelFeaturesVM featuresVM)
        {
            var feature = hotelFeaturesService.UpdateHotelFeatures(id, featuresVM);
            return Ok(feature);
        }
        [HttpPut("Update-Features-RegisteredHotel/{id}/{hotel_id}")]
        public IActionResult UpdateFeatureRegisteredHotel(int id, int hotel_id)
        {
            var feature = hotelFeaturesService.UpdateFeatureRegisteredHotel(id, hotel_id);
            return Ok(feature);
        }
        [HttpDelete("Delete-Feature/{id}")]
        public IActionResult DeleteFeature(int id)
        {
            hotelFeaturesService.RemoveHotelFeatures(id);
            return Ok();
        }
    }
}
