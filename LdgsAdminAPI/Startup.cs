using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using LdgsAdminAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LdgsAdminAPI.Data;
using LdgsAdminAPI.DTO.db;
using LdgsAdminAPI.DTO.request;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Net.Mime;
using System.Reflection;
// ny xvvv
namespace LdgsAdminAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } //PCI product configuration information

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // TVINGA mvc att retunera xml istället för JSON

            //object p = services.AddMvc().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            //JsonConvert.SerializeObject(yourObject,
            //            Newtonsoft.Json.Formatting.None,
            //            new JsonSerializerSettings
            //            {
            //                NullValueHandling = NullValueHandling.Ignore
            //            });
          //  string runPublic = Configuration.GetConnectionString("Ldgs:runPublic");
            //int curSessionTimeOut = int.Parse(Configuration.GetConnectionString("Ldgs:sessionTimeOut"));
            string ConStr = Configuration.GetConnectionString("LdgsAdminAPIContext");
            services.AddDbContext<LdgsAdminDBContext>(options =>
              options.UseSqlServer(ConStr), ServiceLifetime.Transient); 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LdgsAdminAPI", Version = "v1" });
            });

            // Hantera Plymorfism mellan liknande klasser dvs transformera det som är lika i två objekt med snarlika properties mm.
            // Denna services.AddAutoMapper(Assembly.GetExecutingAssembly()); eller nedan!!!! *****
            services.AddAutoMapper(typeof(dbUser));
            services.AddAutoMapper(typeof(reqUser));
            

            services.AddControllers();
            services.AddSingleton<IAdminDbHelper, AdminDbHelper>();
           



            services.AddSingleton<IAdminDbHelper, AdminDbHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LdgsAdminAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
