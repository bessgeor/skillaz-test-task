using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SkillazTestTask.CustomMiddleware;
using System;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace SkillazTestTask
{
	public class Startup
	{
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddCors();
			services.AddMvc();
			MongoUrl url = new MongoUrl( Environment.GetEnvironmentVariable( "MONGODB_URI" ) );
			IMongoClient client = new MongoClient( url );
			IMongoDatabase database = client.GetDatabase( "heroku_8pg0s8ql" );
			services.AddLogging();
			services.AddSingleton( database );
		}

		public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
		{
			loggerFactory.AddNLog();
			app.AddNLogWeb();
			LogManager.Configuration = BuildLogConfiguration();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseCors( builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials()
			);

			app.Use( del => new ErrorHandlingMiddleware( del ).Invoke );

			app.UseMvcWithDefaultRoute();

			LoggingConfiguration BuildLogConfiguration()
				=> new LoggingConfiguration
				{

				};
		}
	}
}