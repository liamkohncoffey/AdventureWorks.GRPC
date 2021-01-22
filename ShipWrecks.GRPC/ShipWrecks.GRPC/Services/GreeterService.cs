using System.Threading.Tasks;
using AdventureWorks.GRPC;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace ShipWrecks.GRPC.Services
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
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayManyHellos(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name}, Number: {i}"
                });
            }
        }

        public override async Task<HelloReply> SayHelloToLastRequest(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            var name = "";

            await foreach (var request in requestStream.ReadAllAsync())
            {
                name = request.Name;
            }
            
            return  new HelloReply
            {
                Message = $"Hello {name}"
            };
        }

        public override async Task SayHelloToEveryRequest(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context)
        {
            await foreach (var request in requestStream.ReadAllAsync())
            {
                await responseStream.WriteAsync(new HelloReply
                {
                    Message = $"Hello {request.Name}"
                });
            }
        }

        public override Task<HelloReply> SayGoodBye(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Bye " + request.Name
            });
        }
    }
}