using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsCatalogManagement.Application;
using ProductsCatalogManagement.Application.Dtos;
using ProductsCatalogManagement.Core.Configuration;
using ProductsCatalogManagement.Infrastructure;

namespace ProductsCatalogManagement.Api
{
    public class Startup
    {
        private const string AllowAllCors = "AllowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureProductsCatalogManagementServices(services);

            services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

            services.AddCors(options =>
                {
                    options.AddPolicy(AllowAllCors,
                                      builder =>
                                      {
                                          builder.AllowAnyHeader();
                                          builder.AllowAnyMethod();
                                          builder.AllowAnyOrigin();
                                      });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowAllCors);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Catalog Management API v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureProductsCatalogManagementServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();

            // Add Core Layer
            services.Configure<ProductsCatalogManagementRunSettings>(Configuration);

            // Add Infrastructure Layer
            services.AddInfrastructure(Configuration);

            // Add Application Layer
            services.AddApplication();

            // Add WebApi Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper
        }
    }
}