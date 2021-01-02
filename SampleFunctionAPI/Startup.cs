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
            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.AddServer(new OpenApiServer { Url = "http://localhost" });
            //    c.EnableAnnotations();
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "Demo API",
            //        Version = "v1",
            //        Description = "Demo API",
            //        Contact = new OpenApiContact
            //        {
            //            Name = "Bill Chesnut",
            //            Email = "bill.chesnut@sixpivot.com.au",
            //            Url = new Uri("https://www.sixpivot.com.au/"),
            //        },
            //    });
            //});

            //SwaggerGenOptions swaggerOptions = new SwaggerGenOptions();
            //swaggerOptions.AddServer(new OpenApiServer { Url = "http://localhost" });
            //swaggerOptions.EnableAnnotations();
            //swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
            //{
            //    Title = "Demo Function API",
            //    Version = "v1",
            //    Description = "Demo Function API",
            //    TermsOfService = new Uri("https://example.com/terms"),
            //    Contact = new OpenApiContact
            //    {
            //        Name = "Bill Chesnut",
            //        Email = "bill.chesnut@sixpivot.com.au",
            //        Url = new Uri("https://www.sixpivot.com.au/"),
            //    },
            //    License = new OpenApiLicense
            //    {
            //        Name = "Use under LICX",
            //        Url = new Uri("https://example.com/license"),
            //    }
            //});
            //builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
            {
                opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
                //opts.ConfigureSwaggerGen(swaggerOptions);
                opts.ConfigureSwaggerGen = (x =>
                {
                    //x.AddServer(new OpenApiServer { Url = "http://localhost" });
                    x.EnableAnnotations();
                    //x.SwaggerDoc("v1", new OpenApiInfo
                    //{
                    //    Title = "Demo Function API",
                    //    Version = "v1",
                    //    Description = "Demo Function API",
                    //    Contact = new OpenApiContact
                    //    {
                    //        Name = "Bill Chesnut",
                    //        Email = "bill.chesnut@sixpivot.com.au",
                    //        Url = new Uri("https://www.sixpivot.com.au/"),
                    //    },
                    //});
                    x.CustomOperationIds(apiDesc =>
                    {
                        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                            ? methodInfo.Name
                            : new Guid().ToString();
                    });
                });
                opts.AddCodeParameter = true;
                opts.PrependOperationWithRoutePrefix = true;
                opts.Documents = new[]
                {
                    new SwaggerDocument
                    {
                        //Name = "v1",
                        Title = "Sample Function API",
                        Version = "v1",
                        Description = "This is an example Sample Function suppling API in .NET Core 3.1"
                        //Contact = new OpenApiContact
                        //{
                        //    Name = "Bill Chesnut",
                        //    Email = "bill.chesnut@sixpivot.com.au",
                        //    Url = new Uri("https://www.sixpivot.com.au/"),
                        //},
                    }
                };
                opts.Title = "Swagger Test";
                //opts.OverridenPathToSwaggerJson = new Uri("http://localhost:7071/api/Swagger/json");
                //opts.ConfigureSwaggerGen = (x =>
                //{
                //    x.CustomOperationIds(apiDesc =>
                //    {
                //        return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)
                //            ? methodInfo.Name
                //            : new Guid().ToString();
                //    });
                //});
            });
        }
    }
}
