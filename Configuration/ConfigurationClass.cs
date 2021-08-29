using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMS_Service_Worker.Common.Services.Configuration
{
    public class ConfigurationClass
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Tarantool Tarantool { get; set; }
        public SMSWorkerSettings SMSWorkerSettings { get; set; }
        public TextNowRoutes TextNowRoutes { get; set; }
        public OtherRoutes OtherRoutes { get; set; }
        public Common Common { get; set; }
        public WorkersTimes WorkersTimes { get; set; }
    }

    public class ConnectionStrings
    {
        public string HangfireConnection { get; set; }
    }

    public class Tarantool
    {
        public string ConnectionCredential { get; set; }
    }

    public class SMSWorkerSettings
    {
        public int SMSWaitTime { get; set; }
        public int TimeBetweenRequests { get; set; }
    }

    public class TextNowRoutes
    {
        public string GetMessageURI { get; set; }
        public string GetProfileURI { get; set; }
    }

    public class OtherRoutes 
    {
        public string CheckIpURI { get; set; }
    }

    public class Common
    {
        public string ErrorResponse { get; set; }
    }

    public class WorkersTimes
    {
        public string CheckDBInsertCron { get; set; }
    }

}
