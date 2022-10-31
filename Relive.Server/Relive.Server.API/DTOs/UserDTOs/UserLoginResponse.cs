namespace Relive.Server.API.DTOs.UserDTOs
{
    public class UserLoginResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JwtToken { get; set; }
    }
}
