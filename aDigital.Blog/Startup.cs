using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aDigital.Blog.Controllers;
using aDigital.Blog.Infra;
using aDigital.Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unity;

namespace aDigital.Blog
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			// Creating the UnityServiceProvider
			var unityServiceProvider = new UnityServiceProvider();

			IUnityContainer container = unityServiceProvider.UnityContainer;

			// Adding the Controller Activator
			// Caution!!! Do this before you Build the ServiceProvider !!!
			services.AddSingleton<IControllerActivator>(new UnityControllerActivator(container));

			//Now build the Service Provider
			var defaultProvider = services.BuildServiceProvider();

			// Configure UnityContainer
			// #region Unity

			//Add the Fallback extension with the default provider
			container.AddExtension(new UnityFallbackProviderExtension(defaultProvider));

			// Register custom Types here

			container.RegisterType<IBlogServices, BlogServices>();
			container.RegisterType<IBlogRepository, BlogRepository>();
			//container.RegisterType<IServer,Kest>
			container.RegisterType<BlogEntriesController>();
			//container.RegisterType<AuthController>();

			// #endregion Unity

			return unityServiceProvider;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}
	}
}
