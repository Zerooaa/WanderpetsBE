using Microsoft.EntityFrameworkCore;
using Wanderpets.Models;

namespace Wanderpets.Models
{
    public class PictureContext: DbContext
    {
        public PictureContext(DbContextOptions<PictureContext> options) : base(options) { }

        public DbSet<PostPicture> PostImages { get; set; }
        public object ProfilePictures { get; internal set; }
    }

}
