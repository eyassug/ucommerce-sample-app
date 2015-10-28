using UCommerce.EntitiesV2;
using UCommerce.Pipelines;
using UCommerce.Pipelines.Initialization;

namespace SampleApp.Extensions.Initialize
{
	/// <summary>
	/// Responsible for executing the addShoeDefinitionPipeline in the Initialize pipeline.
	/// </summary>
	public class AddShoeDefinitionPipelineTask : IPipelineTask<InitializeArgs>
	{
		private readonly Pipeline<ProductDefinition> _addShoeDefinitionPipeline;

		public AddShoeDefinitionPipelineTask(Pipeline<ProductDefinition> addShoeDefinitionPipeline)
		{
			_addShoeDefinitionPipeline = addShoeDefinitionPipeline;
		}

		public PipelineExecutionResult Execute(InitializeArgs subject)
		{
			return _addShoeDefinitionPipeline.Execute(new ProductDefinition());
		}
	}
}
