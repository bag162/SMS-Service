using ProGaudi.MsgPack.Light;

namespace Backend.DBInfrastructure.Models
{
    public class VshardRequestModel
    {
        public int bucket_id { get; set; }
        public string function_name { get; set; }
        public string JsonData { get; set; }
    }
}