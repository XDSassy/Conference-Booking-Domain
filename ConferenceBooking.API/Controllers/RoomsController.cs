using ConferenceBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConferenceBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly List<ConferenceRoom> _rooms;

        public RoomsController(List<ConferenceRoom> rooms)
        {
            _rooms = rooms;
        }

        // GET: api/rooms
        [HttpGet]
        public IActionResult GetAllRooms()
        {
            return Ok(_rooms);
        }

        // GET: api/rooms/{roomNum}
        [HttpGet("{roomNum}")]
        public IActionResult GetRoom(string roomNum)
        {
            var room = _rooms.Find(r => r.RoomNum == roomNum);
            
            if (room == null)
            {
                return NotFound(new { error = $"Room '{roomNum}' not found" });
            }

            return Ok(room);
        }
    }
}