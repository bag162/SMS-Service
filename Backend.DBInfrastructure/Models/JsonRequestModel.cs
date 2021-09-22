using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DBInfrastructure
{
    public class JsonRequestModel<T>
    {
        // standart request
        public string space_name { get; set; }
        public T model { get; set; }
        public Dictionary<int, string> numeric { get; set; }

        // get data by index request
        public string index_name { get; set; }
        public string index_value { get; set; }
        public string type_index { get; set; }

        // get task data
        public int count_tasks { get; set; }

        // get number data
        public int service_id { get; set; }
    }
}