using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SkillazTestTask
{
	public class Program
	{
		public static void Main( string[] args )
		{
			IConfigurationRoot config = new ConfigurationBuilder().AddCommandLine( args ).Build();

			IWebHost host = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot( Directory.GetCurrentDirectory() )
				.UseConfiguration( config )
				.UseStartup<Startup>()
				.Build();

			host.Run();
		}
	}
}