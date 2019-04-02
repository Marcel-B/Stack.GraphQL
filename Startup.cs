using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using com.b_velop.stack.DataContext.Abstract;
using com.b_velop.stack.DataContext.Entities;
using com.b_velop.stack.DataContext.Repository;
using com.b_velop.stack.GraphQl.InputTypes;
using com.b_velop.stack.GraphQl.Middlewares;
using com.b_velop.stack.GraphQl.Resolver;
using com.b_velop.stack.GraphQl.Schemas;
using com.b_velop.stack.GraphQl.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.stack.GraphQl
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public IConfiguration Configuration { get; }

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
                .AddScoped<IDocumentExecuter, DocumentExecuter>()
                .AddScoped<IDocumentWriter, DocumentWriter>()
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
            var authority = string.Empty;
            var apiName = string.Empty;
            if (_env.IsDevelopment())
            {
                var isWindows = System.Runtime.InteropServices.RuntimeInformation
                    .IsOSPlatform(OSPlatform.Windows);
                if (isWindows)
                {
                    services.AddDbContext<MeasureContext>(option =>
                    {
                        option.UseSqlServer(Configuration.GetConnectionString("win"));
                    });
                }
                else
                {
                    services.AddDbContext<MeasureContext>(option =>
                    {
                        option.UseSqlServer(Configuration.GetConnectionString("default"));
                    });
                }
                authority = Configuration.GetSection("ApiSecrets-dev").GetSection("AuthorityUrl").Value;
                apiName = Configuration.GetSection("ApiSecrets-dev").GetSection("ApiName").Value;
            }
            else
            {
                services.AddDbContext<MeasureContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("production"));
                });

                authority = Configuration.GetSection("ApiSecrets").GetSection("AuthorityUrl").Value;
                apiName = Configuration.GetSection("ApiSecrets").GetSection("ApiName").Value;

            }

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = true;
                    options.ApiName = apiName;
                });

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            }); // Add required services for DataLoader support;
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMetricsCollector();
            }
            app.UseAuthentication();
            app.UseGraphQL<ISchema>("/graphql");
        }
    }
}
