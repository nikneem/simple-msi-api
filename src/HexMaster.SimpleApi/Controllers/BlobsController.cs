using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace HexMaster.SimpleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var identity = new DefaultAzureCredential(true);
//            var token = await identity.GetTokenAsync(new TokenRequestContext(new[]{"https://simplemsidemo.blob.core.windows.net"}), CancellationToken.None);
//            var tokenCredential = TokenAcquisitionTokenCredential
            var blobContainerClient = new BlobContainerClient(new Uri("https://simplemsidemo.blob.core.windows.net/images"), identity);
            var blobs = blobContainerClient.GetBlobs();
            foreach (var page in blobs.AsPages())
            {
                foreach (var value in page.Values)
                {
                    Console.WriteLine(value);
                }
            }
            var blobClient =  blobContainerClient.GetBlobClient("works-on-my-machine.jpg");
            var blob = await blobClient.DownloadContentAsync();
            return new FileContentResult(blob.Value.Content.ToArray(), "images/png");
        }
    }
}
