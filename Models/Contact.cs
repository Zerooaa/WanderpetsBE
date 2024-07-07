using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wanderpets.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string UserMessage { get; set; } = "";
        [Column(TypeName = "nvarchar(150)")]
        public string ContactEmail { get; set; } = "";
        [Column(TypeName = "nvarchar(150)")]
        public string ContactName { get; set; } = "";
    }
}
