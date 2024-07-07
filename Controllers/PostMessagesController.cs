using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wanderpets.Models;
using Wanderpets.DTOs;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Wanderpets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostMessagesController : ControllerBase
    {
        private readonly PostMessagesContext _context;

        public PostMessagesController(PostMessagesContext context)
        {
            _context = context;
        }

        // GET: api/PostMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostContentDto>>> GetPostMessages()
        {
            var posts = await _context.PostMessages
                .Select(post => new PostContentDto
                {
                    Id = post.Id,
                    PostMessage = post.PostMessage,
                    PostTag = post.PostTag,
                    PostLocation = post.PostLocation,
                    PostFilter = post.PostFilter,
                    UserId = post.UserId,
                    ImageUrl = post.Images != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(post.Images)}" : null,
                    PostedDate = post.PostedDate,
                    Username = post.User.UserName
                })
                .ToListAsync();

            return Ok(posts);
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PostContentDto>>> GetPostMessagesByUser(string userId)
        {
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return BadRequest("Invalid user ID format");
            }

            var posts = await _context.PostMessages
                .Where(post => post.UserId == parsedUserId)
                .Select(post => new PostContentDto
                {
                    Id = post.Id,
                    PostMessage = post.PostMessage,
                    PostTag = post.PostTag,
                    PostLocation = post.PostLocation,
                    PostFilter = post.PostFilter,
                    UserId = post.UserId,
                    ImageUrl = post.Images != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(post.Images)}" : null,
                    PostedDate = post.PostedDate,
                    Username = post.User.UserName // Fetch the username directly
                })
                .ToListAsync();

            return Ok(posts);
        }
        // GET: api/PostMessages/adopted/{userId}
        [HttpGet("adopted/{userId}")]
        public async Task<ActionResult<IEnumerable<PostContentDto>>> GetAdoptedPostMessagesByUser(string userId)
        {
            var posts = await _context.PostMessages
                .Where(post => post.AdoptedByUserId == userId && post.Adopted)
                .Select(post => new PostContentDto
                {
                    Id = post.Id,
                    PostMessage = post.PostMessage,
                    PostTag = post.PostTag,
                    PostLocation = post.PostLocation,
                    PostFilter = post.PostFilter,
                    UserId = post.UserId,
                    ImageUrl = post.Images != null ? $"data:image/jpeg;base64,{Convert.ToBase64String(post.Images)}" : null,
                    PostedDate = post.PostedDate,
                    Username = post.User.UserName // Fetch the username directly
                })
                .ToListAsync();

            return Ok(posts);
        }
        // GET: api/PostMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostContentDto>> GetPostMessage(int id)
        {
            var post = await _context.PostMessages.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostContentDto
            {
                Id = post.Id,
                PostMessage = post.PostMessage,
                PostTag = post.PostTag,
                PostLocation = post.PostLocation,
                PostFilter = post.PostFilter,
                UserId = post.UserId,
                // Assuming you are storing URLs for images
                ImageUrl = post.Images != null ? $"data:image/png;base64,{Convert.ToBase64String(post.Images)}" : null
            };

            return Ok(postDto);
        }


        [HttpPost]
        public async Task<ActionResult<PostContentDto>> PostPostMessage([FromForm] PostMessagesDTO postDto)
        {
            try
            {
                var postMessage = new PostMessages
                {
                    PostMessage = postDto.PostMessage,
                    PostTag = postDto.PostTag,
                    PostLocation = postDto.PostLocation,
                    PostFilter = postDto.PostFilter,
                    UserId = postDto.UserId,
                    Images = postDto.Images != null ? await ConvertToByteArray(postDto.Images) : null,
                    PostedDate = DateTime.Now,
                    AdoptedByUserId = null // Set to null or handle it appropriately
                };

                _context.PostMessages.Add(postMessage);
                await _context.SaveChangesAsync();

                var resultDto = new PostContentDto
                {
                    Id = postMessage.Id,
                    PostMessage = postMessage.PostMessage,
                    PostTag = postMessage.PostTag,
                    PostLocation = postMessage.PostLocation,
                    PostFilter = postMessage.PostFilter,
                    UserId = postMessage.UserId,
                    ImageUrl = postMessage.Images != null ? $"data:image/png;base64,{Convert.ToBase64String(postMessage.Images)}" : null,
                    PostedDate = postMessage.PostedDate,
                };

                return CreatedAtAction(nameof(GetPostMessagesByUser), new { userId = postMessage.UserId }, resultDto);
            }
            catch (Exception ex)
            {
                var errorMessage = new
                {
                    Message = "An error occurred while processing your request.",
                    Detail = ex.ToString()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        // PUT: api/PostMessages/adopt/{id}
        [HttpPut("adopt/{id}/{userId}")]
        public async Task<IActionResult> AdoptPostMessage(int id, string userId)
        {
            var post = await _context.PostMessages.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            post.Adopted = true;
            post.AdoptedByUserId = userId; // Assuming you have an AdoptedByUserId field in your PostMessages model

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostMessage(int id, [FromBody] PostMessageUpdateDTO postUpdateDto)
        {
            if (id != postUpdateDto.Id)
            {
                return BadRequest();
            }

            var post = await _context.PostMessages.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.PostMessage = postUpdateDto.PostMessage;
            post.PostTag = postUpdateDto.PostTag;

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostMessage(int id)
        {
            var post = await _context.PostMessages.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.PostMessages.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PostMessageExists(int id)
        {
            return _context.PostMessages.Any(e => e.Id == id);
        }

        private async Task<byte[]> ConvertToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
