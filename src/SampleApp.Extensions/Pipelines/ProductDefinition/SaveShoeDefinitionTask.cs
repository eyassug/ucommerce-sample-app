using System.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Save the new ProductDefintion 
	/// if ProductDefinition.Name dosn't exist. 
	/// </summary>
	public class SaveShoeDefinitionTask : IPipelineTask<UCommerce.EntitiesV2.ProductDefinition>
	{
		private readonly IRepository<UCommerce.EntitiesV2.ProductDefinition> _productDefinitionRepository;

		public SaveShoeDefinitionTask(IRepository<UCommerce.EntitiesV2.ProductDefinition> productDefinitionRepository)
		{
			_productDefinitionRepository = productDefinitionRepository;
		}

		public PipelineExecutionResult Execute(UCommerce.EntitiesV2.ProductDefinition subject)
		{
			if (_productDefinitionRepository.Select().Any(x => x.Name == subject.Name)) return PipelineExecutionResult.Success;
						
			subject.Save();

			return PipelineExecutionResult.Success;
		}
	}
}
