using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wanderpets.Models
{
    public class ProfilePicture
    {
        [Key]
        public int ProfileID { get; set; }
        [Column(TypeName = "longblob")]
        public byte[] ProfilePic { get; set; }
    }
}
