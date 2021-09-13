using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DBInfrastructure
{
    public class JsonRequestModel<T>
    {
        public string space_name { get; set; }
        public T model { get; set; }
        public Dictionary<int, string> numeric { get; set; }
    }
}
