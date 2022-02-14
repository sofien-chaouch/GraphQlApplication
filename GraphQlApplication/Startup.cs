using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GraphQlApplication.Data;
using GraphQlApplication.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using GraphQL.Server.Ui.Voyager;
using GraphQlApplication.GraphQL.Command;
using GraphQlApplication.GraphQL.Platforms;
using GraphQlApplication.Interfaces;
using GraphQlApplication.Services;

namespace GraphQlApplication
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration , IWebHostEnvironment env )
        {
            Configuration = configuration; 
            _env = env;
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<PlatformType>()
                .AddType<AddPlatformInputType>()
                .AddType<AddPlatformPayloadType>()
                .AddType<CommandType>()
                .AddType<AddCommandInputType>()
                .AddType<AddCommandPayloadType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GraphQlApplication", Version = "v1"});
            });
            
            services.AddPooledDbContextFactory<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.EnableSensitiveDataLogging(true);
            });
            services.AddCors(option => option.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().
                    AllowAnyOrigin();
            }));
            
            #region DependancyInjection
            
            services.AddTransient<IGenericRepository<Action>, GenericRepository<Action>>();

            services.AddTransient<ISqlHelper, SqlHelper.SqlHelper>();
            services.AddTransient<IHelper, Helper.Helper>();
            services.AddTransient<IActionService, ActionService>();
            
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQlApplication v1"));
                
            }

            //app.UseHttpsRedirection();
            app.UseWebSockets();
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
            
            app.UseCors("CorsPolicy");

            app.UseGraphQLVoyager();

        }
    }
}