using AspirePythonSample.CannyEdgeService.API.Protos;
using FastEndpoints;
using Grpc.Core;
using Grpc.Net.Client;

namespace AspirePythonSample.CannyEdgeService.API.Endpoints;

public class DetectEdges : Endpoint<DetectEdgesRequest, DetectEdgesResponse>
{
    public override void Configure()
    {
        Post("/api/canny-edge-detection");
        AllowAnonymous();
        AllowFileUploads();
    }

    public override async Task HandleAsync(DetectEdgesRequest request, CancellationToken ct)
    {
        if (Files.Count > 0)
        {
            var file = Files[0];

            var imgStream = file.OpenReadStream();

            var channel = GrpcChannel.ForAddress("http://localhost:50051");
            var client = new Protos.CannyEdgeDetector.CannyEdgeDetectorClient(channel);

            // Initialize the client-side streaming call
            using (var call = client.DetectEdges(cancellationToken: ct))
            {
                // First, send the parameters
                var initialRequest = new Protos.DetectEdgesRequest
                {
                    Parameters = new Protos.Parameters
                    {
                        MaxThreshold = request.MaxThreshold,
                        MinThreshold = request.MinThreshold
                    }
                };
                await call.RequestStream.WriteAsync(initialRequest);

                // Then, send the image in chunks
                var buffer = new byte[2048]; // Adjust the size as needed
                int bytesRead;
                while ((bytesRead = await imgStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    var chunkRequest = new Protos.DetectEdgesRequest
                    {
                        ImageChunk = new Protos.ImageChunk { Content = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead) }
                    };
                    await call.RequestStream.WriteAsync(chunkRequest);
                }

                // Complete the request stream
                await call.RequestStream.CompleteAsync();

                // Handle the streamed response
                using (var finalImageStream = new MemoryStream())
                {
                    await foreach (var response in call.ResponseStream.ReadAllAsync())
                    {
                        byte[] imageChunk = response.ImageChunk.ToByteArray();
                        await finalImageStream.WriteAsync(imageChunk, 0, imageChunk.Length);
                    }

                    // After receiving all chunks, the finalImageStream contains the complete image
                    finalImageStream.Seek(0, SeekOrigin.Begin); // Reset the stream position to the beginning

                    // Send the finalImageStream to the user
                    await SendStreamAsync(finalImageStream, cancellation: ct, fileName: request.Filename, contentType: "image/png");
                }
            }
        }

    }
}
