using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Wanderpets.Models;

public class PostMessages
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(150)")]
    public string PostMessage { get; set; } = "";

    [Column(TypeName = "nvarchar(150)")]
    public string PostLocation { get; set; } = "";

    [Column(TypeName = "nvarchar(150)")]
    public string PostTag { get; set; } = "";

    [Column(TypeName = "nvarchar(150)")]
    public string PostFilter { get; set; } = "";

    [Column(TypeName = "longblob")]
    public byte[] Images { get; set; }
    public bool Adopted { get; set; } // New property to track adoption status
    public string? AdoptedByUserId { get; set; }

    public int UserId { get; set; }

    public DateTime PostedDate { get; set; }

    [ForeignKey("UserId")]
    public RegisterDetails User { get; set; }
}