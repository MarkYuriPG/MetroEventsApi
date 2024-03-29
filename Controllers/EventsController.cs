using MetroEventsApi.Contexts;
using MetroEventsApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Drawing;
using System.Security.Claims;

namespace MetroEventsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly MetroEventsDbContext _context;

        public EventsController(MetroEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllEvents")]
        public IActionResult GetAll()
        {
            var events = _context.Events.ToList();
            if (events.Any())
            {
                return Ok(events);
            }else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}", Name = "GetEventbyId")]
        public IActionResult Get(int id)
        {
            var targetEvent = _context.Events
                .Where(e => e.EventId == id)
                .FirstOrDefault();


            if (targetEvent != null)
            {
                return Ok(targetEvent);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("EventName/{eventName}", Name = "GetEventByName")]
        public IActionResult GetByName(string eventName)
        {
            var targetEvent = _context.Events.FirstOrDefault(e => e.EventName == eventName);

            if (targetEvent == null)
            {
                return NotFound($"Event with name '{eventName}' not found.");
            }

            return Ok(targetEvent);
        }

        [HttpPost(Name ="CreateEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Create([FromBody] Event eventt)
        {
            if(eventt == null)
            {
                return BadRequest();
            }

            if(_context.Events.Where(e=>e.EventName!.Equals(eventt.EventName)).FirstOrDefault()!= null)
            {
                return BadRequest($"Event {eventt.EventName} already exist");
            }

            _context.Events.Add(eventt);
            _context.SaveChanges();

            return Ok(eventt);
        }

        [HttpPut(Name = "UpdateEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Update([FromBody] Event eventt)
        {
            if (eventt == null)
            {
                return BadRequest();
            }

            var targetEvent = _context.Events.Where(e => e.EventId == eventt.EventId).FirstOrDefault();

            if (targetEvent == null)
            {
                _context.Events.Add(eventt);
                _context.SaveChanges();
                return Ok(eventt);
            }
            else
            {
                targetEvent.EventName = eventt.EventName;
                targetEvent.EventDescription = eventt.EventDescription;
                targetEvent.Date = eventt.Date;
                targetEvent.Organizer = eventt.Organizer;
                targetEvent.Location = eventt.Location;
                targetEvent.Likes = eventt.Likes;
                targetEvent.Approval = eventt.Approval;
                _context.SaveChanges();
                return Ok(targetEvent);
            }
        }


        [HttpDelete("{id}", Name ="DeleteEvent")]
        [EnableCors("AllowReactApp")]
        public IActionResult Delete(int id)
        {
            var targetEvent = _context.Events.Where(e=>e.EventId == id).FirstOrDefault();

            if( targetEvent == null)
            {
                return NotFound($"No event with id {id} exists.");
            }

            _context.Events.Remove(targetEvent);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
