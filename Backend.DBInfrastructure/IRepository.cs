using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure
{
    public interface IRepository<T> : IDisposable where T: class
    {
        public T FindById<Y>(Y id);
        public IEnumerable<T> FindAll();
        public Task Create(T model);
        public Task Delete(T model);
        public Task Update(T moodel);
    }
}
