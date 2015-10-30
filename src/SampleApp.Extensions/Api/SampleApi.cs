using System;
using System.Linq;
using System.Reflection;
using SampleApp.Extensions.Queries;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;

namespace SampleApp.Extensions.Api
{
	public static class SampleApi
	{
		public static string uCommerceVersion()
		{
			string binPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
			var assemblyName = AssemblyName.GetAssemblyName(string.Format("{0}\\uCommerce.dll", binPath));
			return assemblyName.Version.ToString();
		}

		public static string SchemaVersion()
		{
			var sessionProvider = ObjectFactory.Instance.Resolve<ISessionProvider>();
			var schemaVersionQuery = new SchemaVersionQuery();
			using (var session = sessionProvider.GetSession())
			{
				return schemaVersionQuery.Execute(session).FirstOrDefault().ToString();
			}
		}
	}
}
