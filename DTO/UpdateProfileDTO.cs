namespace Wanderpets.DTO
{
    public class UpdateProfileDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public int UserPhone { get; set; }
    }

}
