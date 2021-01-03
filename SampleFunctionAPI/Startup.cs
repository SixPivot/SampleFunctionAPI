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
                    x.OperationFilter<RemoveCodeQueryParameter>();
                    x.DocumentFilter<CustomSwaggerDocumentAttribute>();
                    //x.AddServer(new OpenApiServer { Url = "http://localhost" });
                    x.EnableAnnotations();
                    x.CustomOperationIds(apiDesc =>
                    {
                        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                            ? methodInfo.Name
                            : new Guid().ToString();
                    });
                });
                opts.AddCodeParameter = true;
                opts.PrependOperationWithRoutePrefix = true;
                opts.Title = "Sample Function API Swagger";
            });
        }
    }

    // used to remove code query parameter (fill fix with backend service in APIM)
    public class RemoveCodeQueryParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters != null)
            {
                foreach(var parameter in operation.Parameters)
                {
                    if (parameter.Name == "code")
                    {
                        operation.Parameters.Remove(parameter);
                        break;
                    }
                }
            }
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
