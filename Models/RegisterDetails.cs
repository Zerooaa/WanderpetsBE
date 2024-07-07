using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Wanderpets.Models
{
    public class RegisterDetails
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string UserEmail { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string UserPassword { get; set; } = "";

        [Column(TypeName = "int(20)")]
        public int UserPhone { get; set; }

        public ICollection<PostMessages> Posts { get; set; } = new List<PostMessages>(); // Initialize as empty collection
    }
}
