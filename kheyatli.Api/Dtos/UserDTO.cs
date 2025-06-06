namespace kheyatli.Api.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ProfilePictureURL { get; set; }
        public UserRole Role { get; set; }
    }
}