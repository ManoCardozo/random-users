using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RandomUser.Domain.Entities;
using System.Collections.Generic;
using RandomUser.Domain.ValueObjects;

namespace RandomUser.Persistence
{
    public static class DatabaseSeeder
    {
        
        /// <summary>
        /// Seeds users table in database
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="rootPath">Root path of application</param>
        /// <param name="seedPath">Path to seed file</param>
        public static void Seed(RandomUserContext context, string rootPath, string seedPath)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var users = ParseUsersJson(rootPath, seedPath);

                context.AddRange(users);
                
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deserializes data from a JSON file into User objects 
        /// </summary>
        private static List<User> ParseUsersJson(string rootPath, string seedPath)
        {
            var jsonFilePath = Path.Combine(rootPath, seedPath);
            var jsonString = File.ReadAllText(jsonFilePath);
            var seed = JsonConvert.DeserializeObject<List<UserSeed>>(jsonString);
            
            var users = new List<User>();

            foreach (var user in seed)
            {
                var imagePath = Path.Combine(rootPath, user.ProfileImagePath);
                byte[] imagedata = File.ReadAllBytes(imagePath);

                var thnumnailPath = Path.Combine(rootPath, user.ProfileThumbnailPath);
                byte[] thumbnailData = File.ReadAllBytes(thnumnailPath);

                var newUser = new User
                {
                    Title = user.Title,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    ProfileImage = imagedata,
                    ProfileThumbnail = thumbnailData
                };

                users.Add(newUser);
            }

            return users;
        }
    }
}
