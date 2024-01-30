using Azure.Storage.Blobs;

namespace Posgrev_Frontend.Logic
{
    public class BlobLogic
    {
        private readonly BlobServiceClient _blobService;

        public BlobLogic(BlobServiceClient blobService)
        {
            _blobService = blobService;
        }

        public async void createContainer(string containerName)
        {
            var containerClient = _blobService.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
        }
    }
}
