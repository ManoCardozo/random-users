using System;
using RandomUser.Application.Extensions;

namespace RandomUser.WebUI.Models
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImageData { get; set; }
        public string ProfileThumbnailData { get; set; }
        public string FullName => $"{Title} {FirstName} {LastName}";
        public string DateOfBirthPretty => DateOfBirth.ToUiDate();
    }
}
