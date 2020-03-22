using System;
using RandomUser.Domain.Entities;
using RandomUser.Repository.GenericRepository;

namespace RandomUser.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        void Commit();
    }
}
