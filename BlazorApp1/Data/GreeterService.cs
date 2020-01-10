using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace GrpcService1
{
    public class GreeterService
    { 
        public async Task<string> SayHello(string name)
        {
            var httpClientHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpClientHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("https://localhost:5001"
                , new GrpcChannelOptions { HttpClient = httpClient });

            var client = new Greeter.GreeterClient(channel);

            var request = await client.SayHelloAsync(new HelloRequest { Name = name });
            return request.Message; 
        }

    }
}
