using System;
using aDigital.Library;
using aDigital.Logging;
using aDigital.Tags.Infra;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aDigital.Tags
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services
				.AddMvc()
				.AddJsonOptions(options => options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore);
			services.AddCors((a) => a.AddPolicy("AllowAnyOrigin", (obj) =>
			{
				obj.AllowAnyOrigin();
				obj.AllowAnyMethod();
				obj.AllowAnyHeader();
			}));
			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAnyOrigin"));
			});

			var builder = new ContainerBuilder();

			// Register dependencies, populate the services from
			// the collection, and build the container. If you want
			// to dispose of the container at the end of the app,
			// be sure to keep a reference to it as a property or field.
			//
			// Note that Populate is basically a foreach to add things
			// into Autofac that are in the collection. If you register
			// things in Autofac BEFORE Populate then the stuff in the
			// ServiceCollection can override those things; if you register
			// AFTER Populate those registrations can override things
			// in the ServiceCollection. Mix and match as needed.
			builder.Populate(services);

			builder.RegisterType<ApplicationInsightsLogger>().As<ILogger>();

			var strConn = Configuration["azureStorageConnectionString"];
			builder.RegisterType<TagsRepository>().As<ITagRepository>().WithParameter(new TypedParameter(typeof(string), strConn ?? "NULL"));
			builder.RegisterType<TagsServices>().As<ITagsService>();
			builder.RegisterType<TagTableEntity>().As<ITag>();
			this.ApplicationContainer = builder.Build();

			// Create the IServiceProvider based on the container.
			return new AutofacServiceProvider(this.ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
			app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
		}
	}
}
