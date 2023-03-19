using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using System;

namespace Timelogger.Api
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));
			services.AddLogging(builder =>
			{
				builder.AddConsole();
				builder.AddDebug();
			});

			services.AddMvc(options => options.EnableEndpointRouting = false);

			if (_environment.IsDevelopment())
			{
				services.AddCors();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseMvc();


			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
			var context = scope.ServiceProvider.GetService<ApiContext>();

			context.Projects.Add(new Project
			{
				Id = 1,
				Name = "e-conomic Interview",
				Status = ProjectStatus.NotInitiated,
				Deadline = new DateTime(2023, 5, 22),
				Entries = new System.Collections.Generic.List<Entry>
				{
					new Entry
					{
						Description = "Some record..",
						Minutes = 20
					}
				}
			});
			context.Projects.Add(new Project
			{
				Id = 2,
				Name = "Project A",
				Status = ProjectStatus.InProgress,
				Deadline = new DateTime(2023, 4, 12),
				Entries = new System.Collections.Generic.List<Entry>
				{
					new Entry
					{
						Description = "Some record..",
						Minutes = 60
					}
				}
			});
			context.Projects.Add(new Project
			{
				Id = 3,
				Name = "Project B",
				Status = ProjectStatus.Completed,
				Deadline = new DateTime(2023, 1, 31),
				Entries = new System.Collections.Generic.List<Entry>
				{
					new Entry
					{
						Description = "Some record..",
						Minutes = 40
					}
				}
			});

			context.SaveChanges();
		}
	}
}