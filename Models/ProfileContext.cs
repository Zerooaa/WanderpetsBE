using Microsoft.EntityFrameworkCore;

namespace Wanderpets.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }

        public DbSet<ProfilePicture> ProfilePictures { get; set; }
    }
}