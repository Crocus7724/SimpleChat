using System;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Builder;
using Owin;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;


namespace Server
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app,ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();
			app.UseMvc(map =>
			{
				map.MapRoute(
					name: "default",
					template: "{controller=Chat}/{action=Index}/{id?}");
			});
			app.UseStaticFiles();
			app.UseAppBuilder(builder => builder.RunSignalR());

		}
	}

	public static class BuilderExtensions
	{
		public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app, Action<IAppBuilder> configure)
		{
			app.UseOwin(addToPipeline =>
			{
				addToPipeline(next =>
				{
					var appBuilder = new AppBuilder();
					appBuilder.Properties["builder.DefaultApp"] = next;

					configure(appBuilder);

					return appBuilder.Build<AppFunc>();
				});
			});

			return app;
		}

		public static void UseSignalR2(this IApplicationBuilder app)
		{
			app.UseAppBuilder(appBuilder => appBuilder.MapSignalR());
		}
	}
}