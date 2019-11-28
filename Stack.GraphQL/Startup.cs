using System.IdentityModel.Tokens.Jwt;
using com.b_velop.stack.DataContext;
using com.b_velop.stack.DataContext.Abstract;
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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace com.b_velop.stack.GraphQl
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
            //.AddSingleton<DataLoaderDocumentListener>()
            //   .AddSingleton<IDocumentExecuter, DocumentExecuter>()
            //   .AddSingleton<IDocumentWriter, DocumentWriter>()
            //    .AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>()
                .AddScoped<MeasureSchema>();
                //.AddScoped<MeasureQuery>()
                //.AddScoped<MeasureMutation>()
                //.AddScoped<MeasureSubscription>()
                //.AddScoped<UnitType>()
                //.AddScoped<UnitInputType>()
                //.AddScoped<LinkType>()
                //.AddScoped<LinkInputType>()
                //.AddScoped<LocationType>()
                //.AddScoped<LocationInputType>()
                //.AddScoped<MeasureValueType>()
                //.AddScoped<MeasureValueInputType>()
                //.AddScoped<PriorityStateType>()
                //.AddScoped<PriorityStateInputType>()
                //.AddScoped<BatteryStateType>()
                //.AddScoped<BatteryStateInputType>()
                //.AddScoped<ActiveMeasurePointType>()
                //.AddScoped<ActiveMeasurePointInputType>()
                //.AddScoped<MeasurePointType>()
                //.AddScoped<MeasurePointInputType>()
                //.AddScoped<TimeTypeInterface>()
                //.AddScoped<ISchema, MeasureSchema>()
                //.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddStackRepositories();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddScoped<IUrlHelper, UrlHelper>(f =>
            {
                var actionContext = f.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            var authority = System.Environment.GetEnvironmentVariable("AuthorityUrl");
            var apiResource = System.Environment.GetEnvironmentVariable("ApiResource");
            var apiScope = System.Environment.GetEnvironmentVariable("ApiScope");

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
            services.AddGraphQL(o => { o.ExposeExceptions = false; }).AddGraphTypes(ServiceLifetime.Scoped);

            //services.AddGraphQL(_ =>
            //{
            //    _.EnableMetrics = true;
            //    _.ExposeExceptions = true;
            //});

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

            services.AddControllers();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (!env.IsDevelopment())
                app.UseAuthentication();

            UpdateDatabase(app);
            app.UseGraphQL<MeasureSchema>();
            //app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings { });
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions { GraphQLEndPoint = "/graphql", Path = "/playground" });
            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(options =>
            {
                options.MapControllers();
            });
            app.UseRouting();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MeasureContext>();
            context.Database.Migrate();
        }
    }
}
