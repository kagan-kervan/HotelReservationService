﻿using HotelReservationService.Data.ViewModels;
using HotelReservationService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypeController : ControllerBase
    {
        private RoomTypeService roomTypeService;
        public RoomTypeController(RoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }
        [HttpGet("get-all")]
        public IActionResult GetAllRoomTypes()
        {
            var list =roomTypeService.GetRoomTypes();
            if(list.Any())
                 return Ok(list);
            return NotFound();

        }
        [HttpGet("get/{id}")]
        public IActionResult GetRoomType(int id)
        {
            var type = roomTypeService.GetRoomTypeWithID(id);
            if(type != null)
                return Ok(type);
            return NotFound();
        }
        [HttpPost("add")]
        public IActionResult AddRoomType([FromBody] RoomTypeVM roomType) 
        {
            var rmtype = roomTypeService.AddRoomType(roomType);
            return Ok(rmtype);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            roomTypeService.RemoveRoomType(id);
            return Ok();
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateRoomType(int id,[FromBody]RoomTypeVM roomType)
        {
            roomTypeService.UpdateRoomType(id, roomType);
            return Ok();
        }
    }
}
