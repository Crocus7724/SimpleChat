using System;
using Microsoft.AspNet.Hosting;

namespace Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplication.Run<Startup>(args);
		}
	}
}