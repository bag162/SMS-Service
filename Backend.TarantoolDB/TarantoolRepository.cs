using Backend.DBInfrastructure;
using Backend.Models.Implementation.Models.MsgPackModels;
using DBInfrastructure;
using ProGaudi.Tarantool.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TarantoolDB
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

        // standart CRUD
        public async Task Create(T model, string bucket_id = null)
        {
            string bucket = (bucket_id == null) ? Space.Name : bucket_id;
            await CallMasterOne(bucket, CRUDOperations.Insert, GenerateJsonData(model));
        }
        public async Task Delete(T model, string bucket_id = null)
        {
            string bucket = (bucket_id == null) ? Space.Name : bucket_id;
            await CallMasterOne(bucket, CRUDOperations.Delete, GenerateJsonData(model));
        }
        public async Task Update(T model, string bucket_id = null)
        {
            string bucket = (bucket_id == null) ? Space.Name : bucket_id;
            await CallMasterArray(bucket, CRUDOperations.Update, GenerateJsonData(model));
        }
        public async Task<IEnumerable<T>> FindAll(string bucket_id = null)
        {
            string bucket = (bucket_id == null) ? Space.Name : bucket_id;
            var result = await CallReplicaArray(bucket, CRUDOperations.GetAll, GenerateJsonData());
            return result.AsEnumerable();
        }

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

        private async Task<T[]> CallReplicaArray(string bucket, CRUDOperations func, JSONRequest jsonParameters)
        {
            var response = await box.Call<(string, string, JSONRequest), T[]>("CallReplica", (bucket, func.ToString(), jsonParameters));
            return response.Data.FirstOrDefault();
        } // only read
        private async Task<T[]> CallMasterArray(string bucket, CRUDOperations func, JSONRequest jsonParameters)
        {
            var response = await box.Call<(string, string, JSONRequest), T[]>("CallMaster", (bucket, func.ToString(), jsonParameters));
            return response.Data.FirstOrDefault();
        } // read/write

        private async Task<T> CallReplicaOne(string bucket, CRUDOperations func, JSONRequest jsonParameters)
        {
            var response = await box.Call<(string, string, JSONRequest), T>("CallReplica", (bucket, func.ToString(), jsonParameters));
            return response.Data.FirstOrDefault();
        } // only read
        private async Task<T> CallMasterOne(string bucket, CRUDOperations func, JSONRequest jsonParameters)
        {
            var response = await box.Call<(string, string, JSONRequest), T>("CallMaster", (bucket, func.ToString(), jsonParameters));
            return response.Data.FirstOrDefault();
        } // read/write

        private JSONRequest GenerateJsonData(T model = null)
        {
            var jsonPropertyes = new Dictionary<int, string>();
            if (model != null)
            {
                var propertyes = model.GetType().GetProperties();

                for (int i = 0; i < propertyes.Length; i++)
                {
                    jsonPropertyes.Add(i + 1, propertyes[i].Name);
                }
            }

            var jsonModel = new JsonRequestModel<T>() { numeric = jsonPropertyes, model = model, space_name = Space.Name };
            return new JSONRequest() { JSON = JsonSerializer.Serialize(jsonModel) };
        }
    }
}
