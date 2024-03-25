using MetroEventsApi.Contexts;
using MetroEventsApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

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

        [HttpGet("Get")]
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

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var targetEvent = _context.Events
                .Where(e => e.EventId == id)
                .FirstOrDefault();
            

            if(targetEvent != null)
            {
                return Ok(targetEvent);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Create")]
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

        [HttpPut("Update")]
        [EnableCors("AllowReactApp")]
        public IActionResult Update([FromBody] Event eventt)
        {
            if(eventt == null)
            {
                return BadRequest();
            }

            var targetEvent = _context.Events.Where(e => e.EventId == eventt.EventId).FirstOrDefault();

            if(targetEvent == null)
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
                _context.SaveChanges();
                return Ok(targetEvent);
            }
        }

        [HttpDelete("Delete/{id}")]
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
