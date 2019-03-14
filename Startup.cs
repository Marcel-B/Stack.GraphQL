﻿using System.IdentityModel.Tokens.Jwt;
using com.b_velop.stack.GraphQl.Contexts;
using com.b_velop.stack.GraphQl.InputTypes;
using com.b_velop.stack.GraphQl.Resolver;
using com.b_velop.stack.GraphQl.Schemas;
using com.b_velop.stack.GraphQl.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.stack.GraphQl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddScoped<MeasureQuery>();
            services.AddScoped<MeasureMutation>();

            services.AddScoped<UnitType>();
            services.AddScoped<UnitInputType>();

            services.AddScoped<MeasureValueType>();
            services.AddScoped<MeasureValueInputType>();

            services.AddScoped<MeasurePointType>();
            services.AddScoped<MeasurePointInputType>();

            services.AddScoped<TimeTypeInterface>();

            services.AddScoped<ISchema, MeasureSchema>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#if DEBUG
            services.AddDbContext<MeasureContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("default"));
            });
#else
            services.AddDbContext<MeasureContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("production"));
            });
#endif
            var authority = Configuration.GetSection("ApiSecrets").GetSection("AuthorityUrl").Value;
            var apiName = Configuration.GetSection("ApiSecrets").GetSection("ApiName").Value;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = true;
                    options.ApiName = apiName;
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
