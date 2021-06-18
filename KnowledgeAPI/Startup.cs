
using KnowledgeAPI.Comm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace KnowledgeAPI
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
            services.AddRazorPages().AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                setupAction.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                setupAction.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddControllers(options =>
            {
                options.Filters.Add<ErrHandle>();
                options.Filters.Add<UrlCheck>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            var v = Res.Types;
            v = Res.Processes;
            v = Res.Grades;
        }
    }
}
