﻿using UCommerce.Pipelines;
using UCommerce.Pipelines.Initialization;

namespace SampleApp.Extensions.Pipelines.Initialize
{
	/// <summary>
	/// Responsible for executing the addShoeDefinitionPipeline in the Initialize pipeline.
	/// </summary>
	public class AddShoeDefinitionPipelineTask : IPipelineTask<InitializeArgs>
	{
		private readonly Pipeline<UCommerce.EntitiesV2.ProductDefinition> _addShoeDefinitionPipeline;

		public AddShoeDefinitionPipelineTask(Pipeline<UCommerce.EntitiesV2.ProductDefinition> addShoeDefinitionPipeline)
		{
			_addShoeDefinitionPipeline = addShoeDefinitionPipeline;
		}

		public PipelineExecutionResult Execute(InitializeArgs subject)
		{
			return _addShoeDefinitionPipeline.Execute(new UCommerce.EntitiesV2.ProductDefinition());
		}
	}
}
