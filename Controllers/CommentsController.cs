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
    public class CommentsController : ControllerBase
    {
        private readonly MetroEventsDbContext _context;

        public CommentsController(MetroEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllComments")]
        public IActionResult Get()
        {
            var comments = _context.Comments.ToList();
            if (comments.Any())
            {
                return Ok(comments);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}", Name = "GetCommentById")]
        public IActionResult Get(int id)
        {
            var targetComment = _context.Comments
                .Where(c=>c.CommentId==id)
                .FirstOrDefault();

            if(targetComment != null)
            {
                return Ok(targetComment);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "CreateComment")]
        [EnableCors("AllowReactApp")]
        public IActionResult Create([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(comment);
        }

        [HttpPut(Name = "UpdateComment")]
        [EnableCors("AllowReactApp")]
        public IActionResult UpdateComment([FromBody] Comment updatedComment)
        {
            if (updatedComment == null)
            {
                return BadRequest();
            }

            var existingComment = _context.Comments.FirstOrDefault(c => c.CommentId == updatedComment.CommentId);

            if (existingComment == null)
            {
                return NotFound("Comment not found.");
            }

            existingComment.Content = updatedComment.Content;
            existingComment.UserId = updatedComment.UserId;

            _context.SaveChanges();

            return Ok(existingComment);
        }

        [HttpDelete("{id}", Name = "DeleteComment")]
        [EnableCors("AllowReactApp")]
        public IActionResult Delete(int id)
        {
            var targetComment = _context.Comments.Where(c => c.CommentId == id).FirstOrDefault();

            if (targetComment == null)
            {
                return NotFound($"No comment with id {id} exists.");
            }

            _context.Comments.Remove(targetComment);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
