using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using AzureFunctions.Extensions.Swashbuckle;
using System.Net.Http;

namespace SampleFunction
{
    public static class SwaggerUIAzureFunc
    {
        [SwaggerIgnore]
        [FunctionName("Swagger")]
        public static Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/json")] HttpRequestMessage req,
            ILogger log, [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
        }

        [SwaggerIgnore]
        [FunctionName("SwaggerUI")]
        public static Task<HttpResponseMessage> Run2(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger/ui")] HttpRequestMessage req,
            ILogger log, [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req, "swagger/json"));
        }

    }
}
