using UCommerce.EntitiesV2;
using UCommerce.Infrastructure.Logging;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.Definitions
{
	public class AddDefinitionPipeline : Pipeline<ProductDefinition>
	{
		public AddDefinitionPipeline(IPipelineTask<ProductDefinition>[] tasks, ILoggingService loggingService) : base(tasks, loggingService)
		{
		}
	}
}
