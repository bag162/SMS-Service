
using Backend.Models.DB;

namespace Backend.Models.Implementation.HistoryInputModels
{
    public class HistoryJsonModel
    {
        // main data
        public string Message { get; set; }
        public int TimeIncident { get; set; }

        public ProxyModel Proxy { get; set; }
        public AccountModel Account { get; set; }
    }
}