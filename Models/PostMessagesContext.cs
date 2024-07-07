using Microsoft.EntityFrameworkCore;

namespace Wanderpets.Models
{
    public class PostMessagesContext : DbContext
    {
        public PostMessagesContext(DbContextOptions<PostMessagesContext> options)
            : base(options)
        {
        }

        public DbSet<PostMessages> PostMessages { get; set; } // Add this line
    }
}