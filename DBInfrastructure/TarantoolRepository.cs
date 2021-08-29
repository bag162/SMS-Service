using DBInfrastructure;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using ProGaudi.Tarantool.Client.Model.Responses;
using ProGaudi.Tarantool.Client.Model.UpdateOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBInfrastructure
{
    public class TRepository<T> : IRepository<T> where T : class
    {
        public ISchema Schema { get; set; }
        public ISpace Space { get; set; }
        public IIndex PrimaryIndex { get; set; }
        protected Box box { get; set; }
        private bool disposedValue;

        public  TRepository( Box box, string vspace, string viindex)
        {
            this.box = box;
            _ = Init(vspace, viindex);
        }
        private async Task Init(string vspace, string viindex)
        {
            Schema = box.GetSchema();
            Space = await box.GetSchema().GetSpace(vspace);
            PrimaryIndex = await Space.GetIndex(viindex);
        }

        public T[] Find<Y>(IIndex index, Y value)
        {
            return index.Select<TarantoolTuple<Y>, T>(TarantoolTuple.Create(value), new SelectOptions { Iterator = Iterator.Eq }).Result.Data;
        }
        public IEnumerable<T> FindAll()
        {
            T[] data;
            try // исправить костыль typeof(T) возможно решение
            {
                data = PrimaryIndex.Select<TarantoolTuple<string>, T>(TarantoolTuple.Create(String.Empty), new SelectOptions { Iterator = Iterator.All }).Result.Data;
            }
            catch
            {
                data = PrimaryIndex.Select<TarantoolTuple<long>, T>(TarantoolTuple.Create(-1L), new SelectOptions { Iterator = Iterator.All }).Result.Data;
            }
            return data;
        }
        public T FindById<Y>(Y id)
        {
            return PrimaryIndex.Select<TarantoolTuple<Y>, T>(TarantoolTuple.Create(id),new SelectOptions { Iterator = Iterator.Eq }).Result.Data.FirstOrDefault();
        }
        public async Task Create(T model)
        {
            if (model.GetType().GetProperty("Id").GetValue(model) == null)
            {
                throw new Exception("В метод Create не передан id модели");
            }
            await PrimaryIndex.Insert(model);
            return;
        }
        public async Task Delete(T model)
        {
            if (model.GetType().GetProperty("Id").GetValue(model) == null)
            {
                throw new Exception("В метод Delete не передан id модели");
            }

            if (model.GetType().GetProperty("Id").GetValue(model).GetType() == typeof(string))
            {
                string id = (string)model.GetType().GetProperty("Id").GetValue(model);
                await PrimaryIndex.Delete<TarantoolTuple<string>, T>(TarantoolTuple.Create(id));
            }
            else
            {
                long id = (long)model.GetType().GetProperty("Id").GetValue(model);
                await PrimaryIndex.Delete<TarantoolTuple<long>, T>(TarantoolTuple.Create(id));
            }
            return;
        }
        public async Task Update(T model)
        {
            var id = model.GetType().GetProperty("Id").GetValue(model);
            string stringId = null;
            long longId = 0;

            if (id == null)
            {
                throw new Exception("В метод Update не передан id модели");
            }

            if (id.GetType() == typeof(string))
            {
                stringId = id.ToString();
            }
            else
            {
                longId = (long)id;
            }

            var listPropertyes = model.GetType().GetProperties();
            for (int i = 1; i < listPropertyes.Length; i++)
            {
                var argument = listPropertyes[i].GetValue(model);

                if (argument == null)
                {
                    continue;
                }

                if (argument.GetType() == typeof(string))
                {
                    string stringArgument = argument.ToString();
                    if (id.GetType() == typeof(string))
                    {
                        await UpdateArgument(stringId, i, stringArgument);
                    }
                    else
                    {
                        await UpdateArgument(longId, i, stringArgument);
                    }
                }

                if (argument.GetType() == typeof(int))
                {
                    int intArgument = (int)argument;
                    if (id.GetType() == typeof(string))
                    {
                        await UpdateArgument(stringId, i, intArgument);
                    }
                    else
                    {
                        await UpdateArgument(longId, i, intArgument);
                    }
                }

                if (argument.GetType() == typeof(double))
                {
                    double doubleArgument = (double)argument;
                    if (id.GetType() == typeof(string))
                    {
                        await UpdateArgument(stringId, i, doubleArgument);
                    }
                    else
                    {
                        await UpdateArgument(longId, i, doubleArgument);
                    }
                }
            }
        }
        public async Task UpdateArgument<Y, Z>(Z modelID, int fieldNumber, Y argument)
        {
            await Space.Update<TarantoolTuple<Z>, T>(TarantoolTuple.Create(modelID), new UpdateOperation[] { UpdateOperation.CreateAssign<Y>(fieldNumber, argument) });
        }

        // no interface func
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                // TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить метод завершения
                // TODO: установить значение NULL для больших полей
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
