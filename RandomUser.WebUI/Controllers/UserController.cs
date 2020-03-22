using System;
using System.Linq;
using RandomUser.WebUI.Models;
using RandomUser.WebUI.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RandomUser.Application.Services;
using RandomUser.Application.Extensions;
using RandomUser.Domain.ValueObjects.ListFilter;

namespace RandomUser.WebUI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult<UserViewModel> Get(Guid userId)
        {
            var user = userService.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            var userviewModel = new UserViewModel
            {
                UserId = user.UserId,
                Title = user.Title,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                ProfileImageData = user.ProfileImage.ToBase64String(),
                ProfileThumbnailData = user.ProfileThumbnail.ToBase64String()
            };

            return Ok(userviewModel);
        }

        [HttpGet]
        public ActionResult<UserViewModel> GetRandom()
        {
            var user = userService.GetRandom();

            if (user == null)
            {
                return NotFound();
            }

            var userviewModel = new UserViewModel
            {
                UserId = user.UserId,
                Title = user.Title,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                ProfileImageData = user.ProfileImage.ToBase64String(),
                ProfileThumbnailData = user.ProfileThumbnail.ToBase64String()
            };

            return Ok(userviewModel);
        }

        [HttpGet]
        [QueryString()]
        public ActionResult<IEnumerable<UserViewModel>> GetList([FromQuery]FilterCriteria filters)
        {
            var users = userService
                .GetList()
                .ToPaged(filters)
                .Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Title = u.Title,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    DateOfBirth = u.DateOfBirth,
                    PhoneNumber = u.PhoneNumber,
                    ProfileImageData = u.ProfileImage.ToBase64String(),
                    ProfileThumbnailData = u.ProfileThumbnail.ToBase64String()
                });

            return Ok(users);
        }

        [HttpPut]
        public ActionResult Update(UserViewModel model)
        {
            var user = userService.Get(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            user.Title = model.Title;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.PhoneNumber = model.PhoneNumber;

            userService.Update(user);

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(Guid userId)
        {
            var user = userService.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            userService.Delete(user);

            return Ok();
        }
    }
}
