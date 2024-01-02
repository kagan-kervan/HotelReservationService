using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewService reviewService { get; set; }
        public ReviewController(ReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        [HttpGet("get-reviews")]
        public IActionResult GetAll() 
        {
            var list = reviewService.GetReviews();
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var rev = reviewService.GetReview(id);
            return Ok(rev);
        }
        [HttpPost("add")]
        public IActionResult AddReview(string comment, int rating)
        {
            DateTime date = DateTime.Now;
            var rev = reviewService.AddReview(comment, rating, DateTime.Parse(date.ToString("d")));
            return Ok(rev);
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateReview(int id,string? comment,int?rating)
        {
            reviewService.UpdateReview(id, comment, rating);
            return Ok();
        }
        [HttpDelete("remove/{id}")]
        public IActionResult Delete(int id)
        {
            reviewService.DeleteReview(id);
            return Ok();
        }
    }
}
