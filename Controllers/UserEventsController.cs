using MetroEventsApi.Contexts;
using MetroEventsApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MetroEventsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly MetroEventsDbContext _context;

        public UserEventsController(MetroEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name ="GetAllUserEvents")]
        public IActionResult Get()
        {
            var userEvents = _context.UserEvents.ToList();
            if(userEvents.Any())
            {
                return Ok(userEvents);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetUsersByEventId/{eventId}", Name = "GetUsersByEventId")]
        public IActionResult GetUsers(int eventId)
        {
            var users = _context.UserEvents
            .Where(ue => ue.EventId == eventId)
            .Select(ue => ue.UserId)
            .ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found for the provided event ID");
            }

            return Ok(users);
        }

        [HttpPost(Name = "CreateUserEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Create(int eventId, int userId)
        {
            // Check if the provided eventId and userId are valid
            var userExists = _context.Users.Any(u => u.UserId == userId);
            var eventExists = _context.Events.Any(e => e.EventId == eventId);

            if (!userExists || !eventExists)
            {
                return BadRequest("User or Event does not exist");
            }

            // Check if the UserEvent association already exists
            var associationExists = _context.UserEvents
                .Any(ue => ue.UserId == userId && ue.EventId == eventId);

            if (associationExists)
            {
                return Conflict("UserEvent association already exists");
            }

            // Create a new UserEvent association
            var newUserEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId
            };

            _context.UserEvents.Add(newUserEvent);
            _context.SaveChanges();

            return Ok(newUserEvent);
        }

        [HttpPut(Name ="UpdateUserEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Update([FromBody] UserEvent userEvent)
        {
            if(userEvent == null)
            {
                return BadRequest();
            }

            var targetUserEvent = _context.UserEvents
                .Where(ue => ue.UserId == userEvent.UserId && 
                ue.EventId == userEvent.EventId)
                .FirstOrDefault();

            if(targetUserEvent == null)
            {
                _context.UserEvents.Add(userEvent);
                _context.SaveChanges();
                return Ok(userEvent);
            }
            else
            {
                targetUserEvent.Status = userEvent.Status;
                _context.SaveChanges();
                return Ok(targetUserEvent);
            }
        }


        [HttpDelete("{userId}/{eventId}", Name = "DeleteUserEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Delete(int userId, int eventId)
        {
            // Find the UserEvent association to delete
            var targetUserEvent = _context.UserEvents.FirstOrDefault(ue => ue.UserId == userId && ue.EventId == eventId);

            if (targetUserEvent == null)
            {
                return NotFound($"UserEvent association with UserId {userId} and EventId {eventId} not found");
            }

            _context.UserEvents.Remove(targetUserEvent);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
