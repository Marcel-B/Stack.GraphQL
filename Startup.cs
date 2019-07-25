using System.IdentityModel.Tokens.Jwt;
using com.b_velop.stack.DataContext.Abstract;
using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using com.b_velop.stack.GraphQl.InputTypes;
using com.b_velop.stack.GraphQl.Middlewares;
using com.b_velop.stack.GraphQl.Resolver;
using com.b_velop.stack.GraphQl.Schemas;
using com.b_velop.stack.GraphQl.Types;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.stack.GraphQl
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
            .AddSingleton<DataLoaderDocumentListener>()
               .AddSingleton<IDocumentExecuter, DocumentExecuter>()
               .AddSingleton<IDocumentWriter, DocumentWriter>()
                .AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>()
                .AddScoped<MeasureQuery>()
                .AddScoped<MeasureMutation>()
                .AddScoped<MeasureSubscription>()
                .AddScoped<UnitType>()
                .AddScoped<UnitInputType>()
                .AddScoped<LinkType>()
                .AddScoped<LinkInputType>()
                .AddScoped<LocationType>()
                .AddScoped<LocationInputType>()
                .AddScoped<MeasureValueType>()
                .AddScoped<MeasureValueInputType>()
                .AddScoped<PriorityStateType>()
                .AddScoped<PriorityStateInputType>()
                .AddScoped<BatteryStateType>()
                .AddScoped<BatteryStateInputType>()
                .AddScoped<ActiveMeasurePointType>()
                .AddScoped<ActiveMeasurePointInputType>()
                .AddScoped<MeasurePointType>()
                .AddScoped<MeasurePointInputType>()
                .AddScoped<TimeTypeInterface>()
                .AddScoped<ISchema, MeasureSchema>()
                .AddScoped<IDataStore<ActiveMeasurePoint>, ActiveMeasurePointRepository>()
                .AddScoped<IDataStore<BatteryState>, BatteryStateRepository>()
                .AddScoped<IDataStore<Location>, LocationRepository>()
                .AddScoped<IDataStore<MeasurePoint>, MeasurePointRepository>()
                .AddScoped<IDataStore<MeasureValue>, MeasureValueRepository>()
                .AddScoped<IDataStore<PriorityState>, PriorityStateRepository>()
                .AddScoped<IDataStore<Link>, LinkRepository>()
                .AddScoped<IDataStore<Unit>, UnitRepository>()
                .AddScoped<ITimeDataStore<MeasureValue>, MeasureValueRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var conString = string.Empty;
            var secretString = string.Empty;
            services.AddScoped<IUrlHelper, UrlHelper>(f =>
            {
                var actionContext = f.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });
            if (_env.IsDevelopment())
            {
                //conString = RuntimeInformation
                //.IsOSPlatform(OSPlatform.Windows) ? "win" : "default";
                conString = "default";
                secretString = "ApiSecrets-dev";
            }
            else
            {
                secretString = "ApiSecrets";
                conString = "production";
            }

            services.AddDbContext<MeasureContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString(conString));
            });

            var authority = Configuration.GetSection(secretString).GetSection("AuthorityUrl").Value;
            var apiName = Configuration.GetSection(secretString).GetSection("ApiName").Value;

            if (!_env.IsDevelopment())
                services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = authority;
                        if (_env.IsDevelopment())
                            options.RequireHttpsMetadata = false;
                        else
                            options.RequireHttpsMetadata = true;
                        options.ApiName = apiName;
                    });

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            }); // Add required services for DataLoader support;

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    //.WithOrigins(
                    //    "http://localhost:8090",
                    //    "http://localhost:8090/",
                    //    "http://localhost/",
                    //    "http://localhost"
                    //    )
                                .AllowCredentials()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                });
            });
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseMetricsCollector();

            if (!env.IsDevelopment())
                app.UseAuthentication();


            //app.UseGraphQL<ISchema>("/graphql");
            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings { });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/graphql", Path = "/playground" });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseSignalR(routes =>
            {
                routes.MapHub<TetsHub>("/chat");
            });
            app.UseMvc();
        }
    }
}
