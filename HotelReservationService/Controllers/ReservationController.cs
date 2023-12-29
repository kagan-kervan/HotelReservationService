using HotelReservationService.Data.Models;
using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using HotelReservationService.Services.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private ReservationService reservationService;
        public ReservationController(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }
        [HttpGet("get-reservations")]
        public IActionResult GetReservations(int? customerIDFilter, int? roomIDFilter, DateTime? dateTime,int pageIndex, int? pageSize)
        {
            ReservationControllerParams reservationControllerParams = new ReservationControllerParams( customerIDFilter, roomIDFilter, dateTime, pageIndex, pageSize);
            var list = reservationService.GetReservations(reservationControllerParams);
            return Ok(list);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            var reserv = reservationService.GetReservation(id);
            return Ok(reserv);
        }
        [HttpGet("get-by-customerID/{customer_id}")]
        public IActionResult GetByCustomerID(int customer_id)
        {
            var reservs = reservationService.GetReservationsFromCustomerID(customer_id);
            return Ok(reservs);
        }
        [HttpGet("get-by-roomID/{room_id}")]
        public IActionResult GetByRoomID(int room_id)
        {
            var reservs = reservationService.GetReservationsFromRoomID(room_id);
            return Ok(reservs);
        }
        [HttpGet("get-by-reviewID/{review_id}")]
        public IActionResult GetByReviewID(int review_id)
        {
            var reserv = reservationService.GetReservationFromReviewId(review_id);
            return Ok(reserv);
        }
        [HttpPost("add/{room_id}/{customer_id}")]
        public IActionResult Post(int room_id,int customer_id, [FromBody]ReservationVM reservationVM) 
        {
            reservationService.AddReservation(room_id, customer_id, reservationVM);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            reservationService.RemoveReservation(id);
            return Ok();
        }
        [HttpDelete("delete-with-customerID/{customer_id}")]
        public IActionResult DeleteWithCustomerID(int customer_id)
        {
            reservationService.RemoveReservationWithCustomerID(customer_id);
            return Ok();
        }
        [HttpDelete("delete-with-roomID/{room_id}")]
        public IActionResult DeleteWithRoomID(int room_id)
        {
            reservationService.RemoveReservationWithRoomID(room_id);
            return Ok();
        }
        [HttpPut("Update/{id}")]
        public IActionResult UpdateReservation(int id, [FromBody]ReservationVM reservationVM)
        {
            reservationService.UpdateReservation(id, reservationVM);
            return Ok();
        }
        [HttpPut("Update-review/{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Review review)
        {
            reservationService.PutReviewToReservation(id, review);
            return Ok();
        }
        [HttpPut("update-room-id/{id}/{room_id}")]
        public IActionResult UpdateReservationRoomID(int id, int room_id)
        {
            reservationService.UpdateReservationRoomID(id, room_id);
            return Ok();
        }
    }
}
