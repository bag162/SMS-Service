

using Models.DTO.DTOModels;

namespace Models.ImplementationModels
{
    public class GetNumberModel
    {
        public bool Success { get; set; }
        public long Service { get; set; }
        public int OrderId { get; set; }
        public int StatusCode { get; set; }
        public string FailMessage { get; set; }
        public string Number { get; set; }
        public UserModel User { get; set; }
    }
}