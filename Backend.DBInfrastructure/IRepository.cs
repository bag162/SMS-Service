using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure
{
    public interface IRepository<T> : IDisposable where T: class
    {
        public Task<IEnumerable<T>> FindAll(string bucket_id = null);
        public Task Create(T model, string bucket_id = null);
        public Task Delete(T model, string bucket_id = null);
        public Task Update(T moodel, string bucket_id = null);
    }
}
