using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService1
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Receive: {request.Name}");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task GetNumbers(GetNumbersRequest request, IServerStreamWriter<GetNumbersResponce> responseStream, ServerCallContext context)
        {
            var rnd = new Random();
            for (int i = 0; i < request.Count; i++)
            {
                await responseStream.WriteAsync(new GetNumbersResponce()
                {
                    Index = i, Number = rnd.Next(100)
                });

                await Task.Delay(5000);
            }
        }
    }
}
