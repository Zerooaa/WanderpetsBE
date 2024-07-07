namespace Wanderpets.DTOs
{
    public class PostMessagesDTO
    {
        public string PostMessage { get; set; }
        public string PostTag { get; set; }
        public string PostLocation { get; set; }
        public string PostFilter { get; set; }
        public int UserId { get; set; }
        public IFormFile? Images { get; set; }
    }
}
