using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Wanderpets.DTO;
using Wanderpets.Models;

namespace Wanderpets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDetailsController : ControllerBase
    {
        private readonly RegisterDetailContext _context;
        private readonly string connectionString = "server=127.0.0.1;database=dbwanderpets;user=root;password=;";
        public RegisterDetailsController(RegisterDetailContext context)
        {
            _context = context;
        }

        // GET: api/RegisterDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterDetails>>> GetRegisterDetails()
        {
            return await _context.RegisterDetails.ToListAsync();
        }

        // GET: api/RegisterDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegisterDetails>> GetRegisterDetails(int id)
        {
            var registerDetails = await _context.RegisterDetails.FindAsync(id);

            if (registerDetails == null)
            {
                return NotFound();
            }

            return registerDetails;
        }

        // PUT: api/RegisterDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegisterDetails(int id, RegisterDetails registerDetails)
        {
            if (id != registerDetails.UserId)
            {
                return BadRequest();
            }

            _context.Entry(registerDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegisterDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(await _context.RegisterDetails.ToListAsync());
        }

        // POST: api/RegisterDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegisterDetails>> PostRegisterDetails(RegisterDetails registerDetails)
        {
            _context.RegisterDetails.Add(registerDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegisterDetails", new { id = registerDetails.UserId }, registerDetails);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterDetails loginDetails)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM RegisterDetails WHERE UserName = @UserName AND UserPassword = @UserPassword";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@UserName", loginDetails.UserName);
                adapter.SelectCommand.Parameters.AddWithValue("@UserPassword", loginDetails.UserPassword);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // User authenticated successfully
                    var user = dt.Rows[0];
                    return Ok(new
                    {
                        message = "Login successful",
                        user = new
                        {
                            UserId = user["UserId"],
                            UserName = user["UserName"]
                        }
                    });
                }
                else
                {
                    // Authentication failed
                    return Unauthorized(new { message = "Invalid username or password" });
                }
            }
        }
        // PUT: api/RegisterDetails/update-profile
        [HttpPut("update-profile/{id}")]
        public IActionResult UpdateProfile(int id, [FromBody] UpdateProfileDTO updateProfile)
        {
            if (id <= 0 || updateProfile == null || id != updateProfile.UserId)
            {
                return BadRequest("Invalid request data.");
            }

            // Find the user by ID and update the properties.
            var user = _context.RegisterDetails.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.UserName = updateProfile.UserName;
            user.FullName = updateProfile.FullName;
            user.UserEmail = updateProfile.UserEmail;
            user.UserPhone = updateProfile.UserPhone;

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/RegisterDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegisterDetails(int id)
        {
            var registerDetails = await _context.RegisterDetails.FindAsync(id);
            if (registerDetails == null)
            {
                return NotFound();
            }

            _context.RegisterDetails.Remove(registerDetails);
            await _context.SaveChangesAsync();

            return Ok(await _context.RegisterDetails.ToListAsync());
        }


        private bool RegisterDetailsExists(int id)
        {
            return _context.RegisterDetails.Any(e => e.UserId == id);
        }
    }
}
