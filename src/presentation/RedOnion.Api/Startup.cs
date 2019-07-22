using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedOnion.Domain;
using RedOnion.Persistence;
using MediatR;
using RedOnion.Application.Helpers.ValidationBehaviors;
using Hangfire;
using AutoMapper;
using Orchestrator.Application.Common.Convinience;
using System.Reflection;

namespace RedOnion.Api
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
            string dbConnectionString = Configuration.GetConnectionString("defaultConnection");

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddHangfire(x => {
                x.UseSqlServerStorage(dbConnectionString);
                
            });

            services.AddApplicationInsightsTelemetry();

             //Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile(Configuration));
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
