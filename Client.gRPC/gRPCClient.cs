using Client.Configuration;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Client.gRPC
{
    public class gRPCClient
    {
        public readonly IOptions<ConfigurationClass> Config;
        public readonly GrpcChannel channel;

        public gRPCClient(IOptions<ConfigurationClass> config)
        {
            this.Config = config;
            this.channel = GrpcChannel.ForAddress(new Uri(Config.Value.gRPC));
        }
    }
}