using HotelListingAPI.Configurations;
using HotelListingAPI.Data;
using HotelListingAPI.IRepository;
using HotelListingAPI.Repository;
using HotelListingAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelListingAPI
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

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))
            );

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // AddCors who is allowed to access API
            services.AddCors(options => {
                options.AddPolicy("AllowAllPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });

            // Mapping data to DTO model (we return DTO from api not plain data)
            services.AddAutoMapper(typeof(MapperInitializer));

            // For controllers
            // AddTransient means every time is needed, a new instance is recreated
            // (fresh copy of IUnitOfWork)
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // AddScope means new instance is created for a period or time of request
            services.AddScoped<IAuthManager, AuthManager>();

            // AddSingleton means one instance will exist for lifetime of application

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListingAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListingAPI v1"));

            app.UseHttpsRedirection();

            app.UseCors("AllowAllPolicy");

            app.UseRouting();

            app.UseAuthentication(); // Authentication
            app.UseAuthorization(); // Then authorization, order matters

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
