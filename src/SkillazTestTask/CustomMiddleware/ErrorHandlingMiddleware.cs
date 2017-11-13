using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SkillazTestTask.CustomMiddleware
{
	public struct ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware( RequestDelegate next )
			=> _next = next;

		public async Task Invoke( HttpContext context )
		{
			try
			{
				await _next( context );
			}
			catch ( Exception ex )
			{
				await HandleExceptionAsync( context, ex );
			}
		}

		private static Task HandleExceptionAsync( HttpContext context, Exception exception )
		{
			throw exception;
		}
	}
}
