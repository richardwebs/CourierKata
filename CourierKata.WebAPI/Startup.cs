using CourierKata.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CourierKata.WebAPI
{
    public class Startup
    {
        private const string XoriginPolicy = "X-OriginPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(x =>
            {
                x.AddPolicy(XoriginPolicy, y =>
                {
                    y.AllowAnyOrigin();
                    y.AllowAnyHeader();
                    y.AllowAnyMethod();
                });
            });

            services.AddSingleton(typeof(IParcelHelper), typeof(ParcelHelper));
            services.AddSingleton(typeof(IParcelService), typeof(ParcelService));
            services.AddSingleton(typeof(IShippingService), typeof(ShippingService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
