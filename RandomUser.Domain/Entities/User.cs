using System;

namespace RandomUser.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] ProfileImage { get; set; }
        public byte[] ProfileThumbnail { get; set; }
    }
}
