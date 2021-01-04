using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using SampleFunction.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using AzureFunctions.Extensions.Swashbuckle.Attribute;

namespace SampleFunction
{
    public static class SampleFunc
    {
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Sample>),StatusCodes.Status200OK)]
        [FunctionName("SampleGetAll")]
        [SwaggerOperation(
            OperationId = "GetSamples",
            Summary = "Get all samples",
            Description = "returns all samples"
        )]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sample")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetSamples - HTTP trigger function processed a request.");

            var rng = new Random();
            List<Sample> samples = new List<Sample>();
            samples.AddRange(Enumerable.Range(1, 5).Select(index => new Sample
            {
                CustomerId = 1,
                FirstName = "FirstName",
                LastName = "LastName"
            })
            .ToArray());

            return new OkObjectResult(samples);
        }
        [Produces("application/json")]
        [ProducesResponseType(typeof(Sample), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [FunctionName("SampleGet")]
        [SwaggerOperation(
            OperationId = "GetSample",
            Summary = "Get specific sample",
            Description = "return a sample"
        )]
        public static async Task<IActionResult> Run1(int id,
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Sample/{id:int}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetSample - HTTP trigger function processed a request.");

            if (id == 5)
            {
                return new NotFoundResult();
            }

            var sample = new Sample
            {
                CustomerId = id,
                FirstName = "FirstName",
                LastName = "LastName"
            };

            return new OkObjectResult(sample);
        }
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [FunctionName("SamplePost")]
        [SwaggerOperation(
            OperationId = "PostSample",
            Summary = "Create a sample",
            Description = "Create a new Sample"
        )]
        public static async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Sample")] [RequestBodyType(typeof(Sample), "sample")] Sample sample,
            ILogger log)
        {
            log.LogInformation("PostSample - HTTP trigger function processed a request.");

            if (sample.CustomerId == 5)
            {
                return new BadRequestResult();
            }

            if (sample.CustomerId == 6)
            {
                return new NotFoundResult();
            }

            return new OkResult();
        }
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [FunctionName("SamplePut")]
        [SwaggerOperation(
            OperationId = "PutSample",
            Summary = "Update a sample",
            Description = "Update an existing Sample"
        )]
        public static async Task<IActionResult> Run3(int id,
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Sample/{id:int}")] [RequestBodyType(typeof(Sample), "sample")] Sample sample,
            ILogger log)
        {
            log.LogInformation("PutSample - HTTP trigger function processed a request.");

            if (sample.CustomerId != id)
            {
                return new BadRequestResult();
            }

            if (sample.CustomerId == 5)
            {
                return new BadRequestResult();
            }

            if (sample.CustomerId == 6)
            {
                return new NotFoundResult();
            }

            return new OkResult();
        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [FunctionName("SampleDelete")]
        [SwaggerOperation(
            OperationId = "DeleteSample",
            Summary = "Delete a sample",
            Description = "Delete an existing Sample"
        )]
        public static async Task<IActionResult> Run4(int id,
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Sample/{id:int}")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DeleteSample - HTTP trigger function processed a request.");

            if (id == 5)
            {
                return new BadRequestResult();
            }

            if (id == 6)
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }
    }
}
