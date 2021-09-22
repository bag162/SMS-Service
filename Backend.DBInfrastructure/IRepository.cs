using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure
{
    public interface IRepository<T> : IDisposable where T: class
    {
        public Task<IEnumerable<T>> FindAll(int bucket_id = 50);
        public Task<T> Create(T model, int bucket_id = 50);
        public Task<T> Delete(T model, int bucket_id = 50);
        public Task<T> Update(T moodel, int bucket_id = 50);

        public Task<IEnumerable<T>> Find<Y>(Y value, int indexId = 1, int bucket = 50);
    }
}