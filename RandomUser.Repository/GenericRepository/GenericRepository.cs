using System;
using System.Linq;
using RandomUser.Persistence;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RandomUser.Repository.GenericRepository
{
	public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
	{
		private readonly RandomUserContext context;
		private readonly DbSet<T> entities;

		public GenericRepository(RandomUserContext context)
		{
			this.context = context;
			this.entities = context.Set<T>();
		}

		public void Dispose()
		{
			if (context != null)
				context.Dispose();
		}

		public virtual T Get(object id)
		{
			return entities.Find(id);
		}

		public virtual IEnumerable<T> GetAll()
		{
			return entities.ToList();
		}

		public virtual void Update(T entity)
		{
			if (entity == null)
				throw new ArgumentNullException("entity");

			entities.Attach(entity);
			context.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(T entity)
		{
			if (context.Entry(entity).State == EntityState.Detached)
			{
				entities.Attach(entity);
			}
			entities.Remove(entity);
		}
	}
}
