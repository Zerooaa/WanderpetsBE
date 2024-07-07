using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wanderpets.Models
{
    public class PostPicture
    {
        [Key]
        public int pictureID { get; set; }
        [Column(TypeName = "longblob")]
        public byte[] Images { get; set; }
    }
}
