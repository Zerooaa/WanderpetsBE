public class PostContentDto
{
    public int Id { get; set; }
    public string PostMessage { get; set; }
    public string PostTag { get; set; }
    public string PostLocation { get; set; }
    public string PostFilter { get; set; }
    public int UserId { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PostedDate { get; set; }  // New field
    public string Username { get; set; }  // New field
}