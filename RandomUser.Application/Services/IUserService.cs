using System;
using System.Collections.Generic;
using RandomUser.Domain.Entities;

namespace RandomUser.Application.Services
{
    public interface IUserService
    {
        User Get(Guid userId);

        User GetRandom();

        IEnumerable<User> GetList();

        void Update(User user);

        void Delete(User user);    
    }
}