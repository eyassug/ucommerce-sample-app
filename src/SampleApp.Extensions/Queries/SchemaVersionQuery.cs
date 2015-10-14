using System.Collections.Generic;
using UCommerce.EntitiesV2.Queries;

namespace SampleApp.Extensions.Queries
{
	public class SchemaVersionQuery : ICannedQuery<int>
	{

		public IEnumerable<int> Execute(NHibernate.ISession session)
		{
			return session.CreateSQLQuery("SELECT SchemaVersion FROM uCommerce_SystemVersion").List<int>();
		}
	}
}
