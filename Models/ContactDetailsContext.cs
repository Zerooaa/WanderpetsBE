using Microsoft.EntityFrameworkCore;

namespace Wanderpets.Models
{
    public class ContactDetailsContext : DbContext
    {
        public ContactDetailsContext(DbContextOptions<ContactDetailsContext> options) : base(options) { }

        public DbSet<Contact> ContactInformation { get; set; }
    }
}