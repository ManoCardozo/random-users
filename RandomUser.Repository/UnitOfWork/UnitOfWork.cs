using System;
using RandomUser.Persistence;
using RandomUser.Domain.Entities;
using RandomUser.Repository.GenericRepository;

namespace RandomUser.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RandomUserContext context;
        private GenericRepository<User> userRepository;

        public UnitOfWork(RandomUserContext context)
        {
            this.context = context;
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.Dispose();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
