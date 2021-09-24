using Backend.DBInfrastructure;
using Backend.DBInfrastructure.Models;
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
        private Dictionary<int, string> Fields { get; set; }
        protected ISchema Schema { get; set; }
        protected ISpace Space { get; set; }
        protected Box box { get; set; }
        private bool disposedValue;

        public TRepository(Box box, string vspace, string viindex, Dictionary<int, string> fields)
        {
            Fields = fields;
            this.box = box;
            Schema = box.GetSchema();
            Task.Run(() => Init(vspace).Wait());
        }

        private async Task Init(string vspace)
        {
            Space = await box.GetSchema().GetSpace(vspace);
        }

        // standart CRUD
        public async Task<T> Create(T model, int bucket = 50)
        {
            var result = await CallMasterOne(GenerateVshardData(GenerateJsonData(model), CRUDOperations.Insert, bucket));
            return result;
        }
        public async Task<T> Delete(T model, int bucket = 50)
        {
            var result = await CallMasterOne(GenerateVshardData(GenerateJsonData(model), CRUDOperations.Delete, bucket));
            return result;
        }
        public async Task<T> Update(T model, int bucket = 50)
        {
            var results = await CallMasterOne(GenerateVshardData(GenerateJsonData(model), CRUDOperations.Update, bucket));
            return results;
        }
        public async Task<IEnumerable<T>> FindAll(int bucket = 50)
        {
            var result = await CallReplicaArray(GenerateVshardData(GenerateJsonData(), CRUDOperations.GetAll, bucket));
            return result.AsEnumerable();
        }
        public async Task<IEnumerable<T>> Find<Y>(Y value, int indexId = 1, int bucket = 50)
        {
            T model = null;
            var s1 = Fields.Where(x => x.Key == indexId).Select(x => x.Value).First();
            var result = await CallReplicaArray(GenerateVshardData(GenerateJsonDataForFindByIndex(value, model, Fields.Where(x => x.Key == indexId).Select(x => x.Value).First()), CRUDOperations.Get, bucket));
            return result.AsEnumerable();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
               /* Space = null; Schema = null; Fields = null; box = null;*/
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки в методе "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected async Task<T[]> CallReplicaArray(string request)
        {
            var response = await box.Call<JsonRequestOneField, T[]>("CallReplica", new JsonRequestOneField() { JsonRequest = request });
            return response.Data.FirstOrDefault();
        } // only read
        protected async Task<T[]> CallMasterArray(string request)
        {
            var response = await box.Call<JsonRequestOneField, T[]>("CallMaster", new JsonRequestOneField() { JsonRequest = request });
            return response.Data.FirstOrDefault();
        } // read/write

        protected async Task<T> CallReplicaOne(string request)
        {
            var response = await box.Call<JsonRequestOneField, T>("CallReplica", new JsonRequestOneField() { JsonRequest = request });
            return response.Data.FirstOrDefault();
        } // only read
        protected async Task<T> CallMasterOne(string request)
        {
            var response = await box.Call<JsonRequestOneField, T>("CallMaster", new JsonRequestOneField() { JsonRequest = request });
            return response.Data.FirstOrDefault();
        } // read/write


        protected string GenerateJsonDataForFindByIndex<Y>(Y index_value, T model = null, string index_name = null)
        {
            while (Space == null)
            {
                Task.Delay(50);
            }
            var jsonPropertyes = new Dictionary<int, string>();
            if (model != null)
            {
                var propertyes = model.GetType().GetProperties();
                for (int i = 0; i < propertyes.Length; i++)
                    jsonPropertyes.Add(i + 1, propertyes[i].Name);
            }
            string typeIndex = (index_value == null) ? null : index_value.GetType().Name.ToLower();

            var jsonModel = new JsonRequestModel<T>() { 
                numeric = jsonPropertyes,
                space_name = Space.Name, 
                index_name = index_name, 
                index_value = index_value.ToString(),
                type_index = typeIndex
            };

            jsonModel.model = (model == null) ? null : model;
            return JsonSerializer.Serialize(jsonModel);
        }
        protected string GenerateJsonData(T model = null)
        {
            while (Space == null)
            {
                Task.Delay(50);
            }
            var jsonPropertyes = new Dictionary<int, string>();
            if (model != null)
            {
                var propertyes = model.GetType().GetProperties();
                for (int i = 0; i < propertyes.Length; i++)
                    jsonPropertyes.Add(i + 1, propertyes[i].Name);
            }

            var jsonModel = new JsonRequestModel<T>()
            {
                numeric = jsonPropertyes,
                space_name = Space.Name
            };
            jsonModel.model = (model == null) ? null : model;
            return JsonSerializer.Serialize(jsonModel);
        }

        protected string GenerateVshardData(string jsonData, CRUDOperations funcname, int bucket_id = 50)
        {
            var vhsardData = new VshardRequestModel() { 
                bucket_id = bucket_id, 
                function_name = funcname.ToString(), 
                JsonData = jsonData };

            return JsonSerializer.Serialize(vhsardData);
        }
        protected string GenerateVshardData(string jsonData, OtherDBOperation funcname, int bucket_id = 50)
        {
            var vhsardData = new VshardRequestModel()
            {
                bucket_id = bucket_id,
                function_name = funcname.ToString(),
                JsonData = jsonData
            };

            return JsonSerializer.Serialize(vhsardData);
        }
    }
}