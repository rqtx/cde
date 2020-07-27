using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cde.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next) {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext) {
			try {
				await _next(httpContext);
			} catch (Exception e) {
				Console.WriteLine(e);
				await HandleExceptionAsync(httpContext);
			}
		}

		private Task HandleExceptionAsync(HttpContext context) {
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			
			return context.Response.WriteAsync(new ErrorDetails {
				StatusCode = context.Response.StatusCode,
				Message = "Internal Server Error"
			}.ToString());
		}
	}
}
