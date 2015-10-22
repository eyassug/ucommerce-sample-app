using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Definitions.Product
{
	public class MergeShoeDefinitionTask : IPipelineTask<ProductDefinition>
	{

		public PipelineExecutionResult Execute(ProductDefinition subject)
		{
			var existingShoeDefinition = ProductDefinition.FirstOrDefault(x => x.Name == subject.Name);

			if(existingShoeDefinition == null) return PipelineExecutionResult.Success;

			MergeProductDefinitions(existingShoeDefinition, subject);

			existingShoeDefinition.Save();

			return PipelineExecutionResult.Success;
		}

		private void MergeProductDefinitions(ProductDefinition existingShoeDefinition, ProductDefinition newProductDefinition)
		{
			newProductDefinition.ProductDefinitionFields.ForEach(existingShoeDefinition.AddProductDefinitionField);
		}
	}
}
