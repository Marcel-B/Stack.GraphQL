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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace com.b_velop.stack.GraphQl
{
    public class Startup
    {
        private readonly IHostEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(
            IConfiguration configuration,
            IHostEnvironment env)
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
                .AddScoped<BatteryStateRepository>()
                .AddScoped<IDataStore<Location>, LocationRepository>()
                .AddScoped<IDataStore<MeasurePoint>, MeasurePointRepository>()
                .AddScoped<IDataStore<MeasureValue>, MeasureValueRepository>()
                .AddScoped<IDataStore<PriorityState>, PriorityStateRepository>()
                .AddScoped<IDataStore<Link>, LinkRepository>()
                .AddScoped<IDataStore<Unit>, UnitRepository>()
                .AddScoped<ITimeDataStore<MeasureValue>, MeasureValueRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddScoped<IUrlHelper, UrlHelper>(f =>
            {
                var actionContext = f.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            var authority = System.Environment.GetEnvironmentVariable("AuthorityUrl");
            var apiResource = System.Environment.GetEnvironmentVariable("ApiResource");
            var apiScope = System.Environment.GetEnvironmentVariable("ApiScope");
            var conString = System.Environment.GetEnvironmentVariable("ConString");

            services.AddDbContext<MeasureContext>(option =>
            {
                option.UseSqlServer(conString);
            });

            if (!_env.IsDevelopment())
                services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = authority;
                        if (_env.IsDevelopment())
                            options.RequireHttpsMetadata = false;
                        else
                            options.RequireHttpsMetadata = true;
                        options.ApiName = apiResource;
                    });

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder
                    //.AllowAnyOrigin()
                        .WithOrigins(
                            "http://localhost",
                            "https://nuqneh.de")
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddSignalR();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (!env.IsDevelopment())
                app.UseAuthentication();


            //app.UseGraphQL<ISchema>("/graphql");
            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings { });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/graphql", Path = "/playground" });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(options =>
            {
                options.MapHub<TetsHub>("/chat");
            });
            app.UseRouting();
        }
    }
}
