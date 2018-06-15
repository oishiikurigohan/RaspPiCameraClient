using Newtonsoft.Json;

namespace RaspPiCameraClient
{
    public class RequestJsonModel
    {
        [JsonProperty("ContainerName")]
        public string containerName { get; set; }

        [JsonProperty("BlobName")]
        public string blobName { get; set; }

        [JsonProperty("Permission")]
        public string permission { get; set; }
    }
}