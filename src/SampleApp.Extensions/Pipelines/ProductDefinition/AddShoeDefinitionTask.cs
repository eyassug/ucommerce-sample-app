using System.Linq;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Save the new ProductDefintion 
	/// if ProductDefinition.Name dosn't exist. 
	/// </summary>
	public class AddShoeDefinitionTask : IPipelineTask<UCommerce.EntitiesV2.ProductDefinition>
	{ 
		public PipelineExecutionResult Execute(UCommerce.EntitiesV2.ProductDefinition subject)
		{
			if (UCommerce.EntitiesV2.ProductDefinition.All().Any(x => x.Name == subject.Name)) return PipelineExecutionResult.Success;
						
			subject.Save();

			return PipelineExecutionResult.Success;
		}
	}
}
