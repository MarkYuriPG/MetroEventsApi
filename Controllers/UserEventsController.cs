using MetroEventsApi.Contexts;
using MetroEventsApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        //[HttpGet("{eventId}", Name = "GetUserbyEventId")]
        //public IActionResult GetUsers(int id)
        //{
        //    var users = _context.UserEvents
        //                        .Where(ue => ue.EventId == id)
        //                        .Select(ue => ue.User)
        //                        .ToList();

        //    if (users == null || !users.Any())
        //    {
        //        return NotFound($"No users found for event with ID {id}");
        //    }

        //    return Ok(users);
        //}

        //[HttpGet("{userId}", Name = "GetEventsbyUserId")]
        //public IActionResult GetEvents(int id)
        //{
        //    var events = _context.UserEvents
        //                        .Where(ue => ue.UserId == id)
        //                        .Select(ue => ue.Event)
        //                        .ToList();

        //    if (events == null || !events.Any())
        //    {
        //        return NotFound($"No users found for event with ID {id}");
        //    }

        //    return Ok(events);
        //}

        [HttpPost(Name = "CreateOrUpdateUserEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult CreateOrUpdate(int eventId, int userId)
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
