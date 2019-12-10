using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Wto.Services.Members.Service.DbContext;

namespace Wto.Services.Members.Service {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			// Add dependency injection
			services
				.AddScoped<AppDbContext, AppDbContext>();

			// Configure Application to use SqlServer
			services.AddDbContext<AppDbContext>(options => {
				options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
			});

			// Register the Swagger services
			services.AddSwaggerDocument(options => {
				options.Title = "WTO Members Service";
			});

			// Enable CORS
			services.AddCors();

			// Configure Mediatr
			// Mediatr Settings
			services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
				.AddJsonOptions(options => {
					// Use camel case properties in the serializer and the spec (optional)
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					// Use string enums in the serializer and the spec (optional)
					options.SerializerSettings.Converters.Add(new StringEnumConverter());
				});

			// Add AutoMapper
			services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			// Register the Swagger generator and the Swagger UI middlewares
			app.UseOpenApi();
			app.UseSwaggerUi3(options => {
				options.Path = "/api/swagger";
			});

			app.UseCors(options => {
				options
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials()
					.WithExposedHeaders("Location");
			});

			app.UseMvc();
		}
	}
}
