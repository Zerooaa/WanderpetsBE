using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wanderpets.DTOs;
using Wanderpets.Models;

namespace Wanderpets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostPicturesController : ControllerBase
    {
        private readonly PictureContext _context;

        public PostPicturesController(PictureContext context)
        {
            _context = context;
        }

        // GET: api/PostPictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostPicture>>> GetPostImages()
        {
            return await _context.PostImages.ToListAsync();
        }

        // GET: api/PostPictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostPicture>> GetPostPicture(int id)
        {
            var postPicture = await _context.PostImages.FindAsync(id);

            if (postPicture == null)
            {
                return NotFound();
            }

            return postPicture;
        }

        // POST: api/PostPictures/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] PostMessagesDTO postMessagesDto)
        {
            if (postMessagesDto.Images == null || postMessagesDto.Images.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await postMessagesDto.Images.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            var postPicture = new PostPicture
            {
                Images = fileBytes
            };

            _context.PostImages.Add(postPicture);
            await _context.SaveChangesAsync();

            return Ok(postPicture.pictureID); // Return the ID of the uploaded image
        }

        // POST: api/PostPictures/GetImageUrls
        [HttpPost("GetImageUrls")]
        public async Task<IActionResult> GetImageUrls([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("No picture IDs provided.");
            }

            var imageUrls = new Dictionary<int, string>();

            foreach (var id in ids)
            {
                var postPicture = await _context.PostImages.FindAsync(id);
                if (postPicture != null)
                {
                    try
                    {
                        var base64String = Convert.ToBase64String(postPicture.Images);
                        var imageUrl = $"data:image/png;base64,{base64String}"; // Adjust MIME type if necessary
                        imageUrls.Add(id, imageUrl);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error converting image for ID {id}: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"No image found for ID {id}");
                }
            }

            return Ok(imageUrls);
        }

        // DELETE: api/PostPictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostPicture(int id)
        {
            var postPicture = await _context.PostImages.FindAsync(id);
            if (postPicture == null)
            {
                return NotFound();
            }

            _context.PostImages.Remove(postPicture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostPictureExists(int id)
        {
            return _context.PostImages.Any(e => e.pictureID == id);
        }
    }
}
