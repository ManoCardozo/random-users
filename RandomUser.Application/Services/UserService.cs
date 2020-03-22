using System;
using System.Linq;
using RandomUser.Domain.Entities;
using System.Collections.Generic;
using RandomUser.Repository.UnitOfWork;

namespace RandomUser.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User Get(Guid userId)
        {
            return unitOfWork
                .UserRepository
                .Get(userId);
        }

        public User GetRandom()
        {
            return unitOfWork
                    .UserRepository
                    .GetAll()
                    .OrderBy(o => Guid.NewGuid())
                    .FirstOrDefault();
        }

        public IEnumerable<User> GetList()
        {
            return unitOfWork
                .UserRepository
                .GetAll();
        }

        public void Update(User user)
        {
            unitOfWork
                .UserRepository
                .Update(user);
        }

        public void Delete(User user)
        {
            unitOfWork
                .UserRepository
                .Delete(user);
        }
    }
}
