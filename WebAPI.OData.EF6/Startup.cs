using AutoMapper;
using DAL.EF6;
using Domain.OData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WebAPI.OData.EF6.Binders;

namespace WebAPI.OData.EF6
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
            services.AddControllers()
                .AddOData(opt => opt.EnableQueryFeatures()
                    .AddRouteComponents("", GetEdmModel(), services => services.AddSingleton<ISearchBinder, OpsTenantSearchBinder>()));
            services.AddScoped
            (
                _ => new MyDbContext
                (
                    Configuration.GetConnectionString("DefaultConnection")
                )
            )
            .AddSingleton<AutoMapper.IConfigurationProvider>
            (
                new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(Startup).Assembly);
                    cfg.AllowNullCollections = true;
                })
            )
            .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            //builder.Namespace = "com.FooBar";
            builder.EntitySet<OpsTenant>(nameof(OpsTenant));
            builder.EntitySet<CoreBuilding>(nameof(CoreBuilding));
            builder.EntitySet<OpsBuilder>(nameof(OpsBuilder));
            builder.EntitySet<OpsCity>(nameof(OpsCity));

            return builder.GetEdmModel();
        }
    }
}
