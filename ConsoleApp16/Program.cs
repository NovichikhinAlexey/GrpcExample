using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;

namespace ConsoleApp16
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var resp = await client.SayHelloAsync(new HelloRequest() {Name = "Alex"});

            Console.WriteLine(resp.Message);

            using (var stream = client.GetNumbers(new GetNumbersRequest() { Count = 10 }))
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    var num = stream.ResponseStream.Current;
                    Console.WriteLine($"{num.Index} = {num.Number}");
                }
            }

            Console.ReadLine();
        }
    }
}
