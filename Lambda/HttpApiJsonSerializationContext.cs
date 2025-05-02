using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json.Serialization;

namespace CreationSharingPlatform.Lambda
{
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
    [JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
    public partial class HttpApiJsonSerilalizerContext : JsonSerializerContext
    {
    }
}
