using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.API.Comman;
using CORE.API.DBcontext;
using CORE.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Integrations.JsonDotNet.Converters;
using WebApiContrib.Core.Formatter.Bson;

namespace CORE.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("SamplePolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<Settings>(
                   options =>
                   {
                       options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                       options.Database = Configuration.GetSection("MongoDb:Database").Value;
                   });

            services.AddSingleton<IMongoClient, MongoClient>(
               _ => new MongoClient(Configuration.GetSection("MongoDb:ConnectionString").Value));
            services.AddTransient<IShopContext, ShopContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    // Adds automatic json parsing to BsonDocuments.
            //    options.SerializerSettings.Converters.Add(new BsonArrayConverter());
            //    options.SerializerSettings.Converters.Add(new BsonMinKeyConverter());
            //    options.SerializerSettings.Converters.Add(new BsonBinaryDataConverter());
            //    options.SerializerSettings.Converters.Add(new BsonNullConverter());
            //    options.SerializerSettings.Converters.Add(new BsonBooleanConverter());
            //    options.SerializerSettings.Converters.Add(new BsonObjectIdConverter());
            //    options.SerializerSettings.Converters.Add(new BsonDateTimeConverter());
            //    options.SerializerSettings.Converters.Add(new BsonRegularExpressionConverter());
            //    options.SerializerSettings.Converters.Add(new BsonDocumentConverter());
            //    options.SerializerSettings.Converters.Add(new BsonStringConverter());
            //    options.SerializerSettings.Converters.Add(new BsonDoubleConverter());
            //    options.SerializerSettings.Converters.Add(new BsonSymbolConverter());
            //    options.SerializerSettings.Converters.Add(new BsonInt32Converter());
            //    options.SerializerSettings.Converters.Add(new BsonTimestampConverter());
            //    options.SerializerSettings.Converters.Add(new BsonInt64Converter());
            //    options.SerializerSettings.Converters.Add(new BsonUndefinedConverter());
            //    options.SerializerSettings.Converters.Add(new BsonJavaScriptConverter());
            //    options.SerializerSettings.Converters.Add(new BsonValueConverter());
            //    options.SerializerSettings.Converters.Add(new BsonJavaScriptWithScopeConverter());
            //    options.SerializerSettings.Converters.Add(new BsonMaxKeyConverter());
            //    options.SerializerSettings.Converters.Add(new ObjectIdConverter());
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("SamplePolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            //   app.UseHttpsRedirection();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    // log the exception etc..
                    // produce some response for the caller
                });
            });
            app.UseMvc();
        }
    }
}
