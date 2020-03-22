using System;

namespace RandomUser.Domain.ValueObjects
{
    public class UserSeed
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImagePath { get; set; }
        public string ProfileThumbnailPath { get; set; }
    }
}
