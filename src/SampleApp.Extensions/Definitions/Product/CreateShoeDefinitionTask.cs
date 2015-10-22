using System;
using System.Collections.Generic;
using UCommerce.EntitiesV2;
using UCommerce.Extensions;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Definitions.Product
{
	public class CreateShoeDefinitionTask : IPipelineTask<ProductDefinition>
	{

		public PipelineExecutionResult Execute(ProductDefinition subject)
		{
			subject = new ProductDefinition()
			{
				Name = "Shoe",
				CreatedOn = DateTime.Now,
				CreatedBy = "Mads",
				ModifiedOn = DateTime.Now,
				ModifiedBy = "Mads",

			};

			var definitionsFields = CreateDefinitionFields();
			definitionsFields.ForEach(subject.AddProductDefinitionField);
			
			return PipelineExecutionResult.Success;
		}

		private IList<ProductDefinitionField> CreateDefinitionFields()
		{
			return new List<ProductDefinitionField>
			{
				new ProductDefinitionField()
				{
					DataType = DataType.FirstOrDefault(x => x.DataTypeId == 6),
					Name = "Awesomeness",
					RenderInEditor = true,
				},
				new ProductDefinitionField()
				{
					DataType = DataType.FirstOrDefault(x => x.DataTypeId == 1),
					Name = "Short story",
					RenderInEditor = true,
				}
			};
		}
	}
}
