using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MetroEventsApi.Contexts;
using MetroEventsApi.Models;
using Microsoft.AspNetCore.Cors;

namespace MetroEventsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MetroEventsDbContext _context;

        public UsersController(MetroEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList();
            if (users.Any())
            {
                return Ok(users);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult Get(int id)
        {
            var targetUser = _context.Users
                .Where(e => e.UserId == id)
                .FirstOrDefault();


            if (targetUser != null)
            {
                return Ok(targetUser);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "CreateUser")]
        [EnableCors("AllowReactApp")]
        public IActionResult Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (_context.Users.Where(e => e.UserName!.Equals(user.UserName)).FirstOrDefault() != null)
            {
                return BadRequest($"Event {user.UserName} already exist");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPut(Name ="UpdateUser")]
        [EnableCors("AllowReactApp")]
        public IActionResult Update([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var targetUser = _context.Users.Where(e => e.UserId == user.UserId).FirstOrDefault();

            if (targetUser == null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok(user);
            }
            else
            {
                targetUser.UserName = user.UserName;
                targetUser.Password = user.Password;
                targetUser.Role = user.Role;
                targetUser.Status = user.Status;
                _context.SaveChanges();
                return Ok(targetUser);
            }
        }

        [HttpDelete("{id}", Name ="DeleteUser")]
        [EnableCors("AllowReactApp")]
        public IActionResult Delete(int id)
        {
            var targetUser = _context.Users.Where(e => e.UserId == id).FirstOrDefault();

            if (targetUser == null)
            {
                return NotFound($"No user with id {id} exists.");
            }

            _context.Users.Remove(targetUser);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("Login/{username}&{password}")]
        [EnableCors("AllowReactApp")]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid login request.");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user == null)
            {
                return NotFound("User not found or invalid credentials.");
            }

            // You can return additional information about the user if needed
            // For simplicity, returning only the user ID here
            return Ok(new { UserId = user.UserId,  UserRole = user.Role });
        }
    }
}
