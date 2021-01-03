using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi;
using AzureFunctions.Extensions.Swashbuckle.Settings;

[assembly: FunctionsStartup(typeof(SampleFunction.Startup))]

namespace SampleFunction
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
            {
                opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
                opts.ConfigureSwaggerGen = (x =>
                {
                    x.DocumentFilter<CustomSwaggerDocumentAttribute>();
                    x.EnableAnnotations();
                    x.CustomOperationIds(apiDesc =>
                    {
                        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                            ? methodInfo.Name
                            : new Guid().ToString();
                    });
                });
                // remove code as query parameter
                opts.AddCodeParameter = false;
                // see host.json to remove /api/ from route prefix
                opts.PrependOperationWithRoutePrefix = true;
                opts.Title = "Sample Function API Swagger";
            });
        }
    }

     public class CustomSwaggerDocumentAttribute : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Info = new OpenApiInfo
            {
                Title = "Sample Function API",
                Version = "v1",
                Description = "Sample Function API",
                Contact = new OpenApiContact
                {
                    Name = "Bill Chesnut",
                    Email = "bill.chesnut@sixpivot.com.au",
                    Url = new Uri("https://www.sixpivot.com.au/"),
                },
            };
        }
    }
}
