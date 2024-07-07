using Microsoft.EntityFrameworkCore;

namespace Wanderpets.Models
{
    public class RegisterDetailContext: DbContext
    {
        public RegisterDetailContext(DbContextOptions<RegisterDetailContext> options): base(options)
        {
        }
        public DbSet<RegisterDetails> RegisterDetails { get; set; }
    }
}
