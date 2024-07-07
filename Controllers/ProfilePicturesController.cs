using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wanderpets.DTOs;
using Wanderpets.Models;

namespace Wanderpets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilePicturesController : ControllerBase  // Renamed controller to match route
    {
        private readonly ProfileContext _context;

        public ProfilePicturesController(ProfileContext context)
        {
            _context = context;
        }

        // GET: api/ProfilePictures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilePicture>>> GetProfilePictures()  // Corrected method name
        {
            return await _context.ProfilePictures.ToListAsync();
        }

        // GET: api/ProfilePictures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfilePicture>> GetProfilePicture(int id)
        {
            var profilePicture = await _context.ProfilePictures.FindAsync(id);

            if (profilePicture == null)
            {
                return NotFound();
            }

            return profilePicture;
        }

        // PUT: api/ProfilePictures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfilePicture(int id, ProfilePicture profilePicture)
        {
            if (id != profilePicture.ProfileID)
            {
                return BadRequest();
            }

            _context.Entry(profilePicture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilePictureExists(id))
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

        // POST: api/ProfilePictures/upload
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ProfilePictureDTO profilePictureDto)
        {
            if (profilePictureDto.ProfilePicture == null || profilePictureDto.ProfilePicture.Length == 0)
                return BadRequest("No file uploaded.");

            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                await profilePictureDto.ProfilePicture.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            var profilePicture = await _context.ProfilePictures.FindAsync(profilePictureDto.ProfileID);
            if (profilePicture == null)
            {
                profilePicture = new ProfilePicture
                {
                    ProfileID = profilePictureDto.ProfileID,
                    ProfilePic = fileBytes
                };
                _context.ProfilePictures.Add(profilePicture);
            }
            else
            {
                profilePicture.ProfilePic = fileBytes;
                _context.ProfilePictures.Update(profilePicture);
            }

            await _context.SaveChangesAsync();

            return Ok(new { profilePic = Convert.ToBase64String(fileBytes) });
        }

        // DELETE: api/ProfilePictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfilePicture(int id)
        {
            var profilePicture = await _context.ProfilePictures.FindAsync(id);
            if (profilePicture == null)
            {
                return NotFound();
            }

            _context.ProfilePictures.Remove(profilePicture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilePictureExists(int id)
        {
            return _context.ProfilePictures.Any(e => e.ProfileID == id);
        }
    }
}