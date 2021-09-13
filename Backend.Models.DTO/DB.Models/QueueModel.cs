using ProGaudi.MsgPack.Light;

namespace Backend.Models.DB.Models
{
    public class QueueModel
    {
        [MsgPackArrayElement(0)]
        public string Id { get; set; }
        [MsgPackArrayElement(1)]
        public int Type { get; set; }
        [MsgPackArrayElement(2)]
        public string Data { get; set; }
        [MsgPackArrayElement(3)]
        public int Priority { get; set; }
        [MsgPackArrayElement(4)]
        public string StartTime { get; set; }
        [MsgPackArrayElement(5)]
        public string LastCheck { get; set; }
        [MsgPackArrayElement(6)]
        public string Bucket { get; set; }
    }
}