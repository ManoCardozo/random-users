using System.Collections.Generic;

namespace RandomUser.Repository.GenericRepository
{
	public interface IGenericRepository<T> where T : class
	{
		T Get(object id);

		IEnumerable<T> GetAll();

		void Update(T entity);

		void Delete(T entity);
	}
}
