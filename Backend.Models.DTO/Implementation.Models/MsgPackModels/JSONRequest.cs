using ProGaudi.MsgPack.Light;

namespace Backend.Models.Implementation.Models.MsgPackModels
{
    public class JSONRequest
    {
        [MsgPackArrayElement(0)]
        public string JSON { get; set; }
    }
}
