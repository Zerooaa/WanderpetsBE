using Microsoft.AspNetCore.Http;

namespace Wanderpets.DTOs
{
    public class ProfilePictureDTO
    {
        public int ProfileID { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}