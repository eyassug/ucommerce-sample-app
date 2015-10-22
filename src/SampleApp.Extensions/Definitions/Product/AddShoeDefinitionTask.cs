using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Definitions.Product
{
	public class AddShoeDefinitionTask : IPipelineTask<ProductDefinition>
	{ 

		public PipelineExecutionResult Execute(ProductDefinition subject)
		{
			if (ProductDefinition.All().Any(x => x.Name == subject.Name)) return PipelineExecutionResult.Success;
						
			subject.Save();

			return PipelineExecutionResult.Success;
		}
	}
}
