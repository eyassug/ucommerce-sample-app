using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCommerce.EntitiesV2;
using UCommerce.EntitiesV2.Definitions;
using UCommerce.Extensions;
using UCommerce.Pipelines;

namespace SampleApp.Extensions.Pipelines.ProductDefinition
{
	/// <summary>
	/// Modifies the ProductDefinition with content like
	/// Name, DefinitionFields and DefinitionFieldDescriptions. 
	/// </summary>
	public class ModifyShoeDefinitionTask : IPipelineTask<UCommerce.EntitiesV2.ProductDefinition>
	{
		public PipelineExecutionResult Execute(UCommerce.EntitiesV2.ProductDefinition subject)
		{
			subject.Name = "Shoe";
			subject.CreatedOn = DateTime.Now;
			subject.CreatedBy = "Mads";
			subject.ModifiedOn = DateTime.Now;
			subject.ModifiedBy = "Mads";

			var definitionsFields = CreateDefinitionFields();
			definitionsFields.ForEach(subject.AddProductDefinitionField);
			
			return PipelineExecutionResult.Success;
		}

		private IEnumerable<ProductDefinitionField> CreateDefinitionFields()
		{
			return new List<ProductDefinitionField>
			{
				CreateProductDefinitionField("Awesomeness", new BooleanDataTypeDefinition()),
				CreateProductDefinitionField("ShortStory", new ShortTextDataTypeDefinition())
			};
		}

		private ProductDefinitionField CreateProductDefinitionField(string name, DataTypeDefinition definition)
		{
			var productDefinitionField = new ProductDefinitionField()
			{
				DataType = DataType.FirstOrDefault(x => x.DefinitionName == definition.Name),
				Name = name,
				RenderInEditor = true,
			};
			
			var descriptions = CreateDefinitionFieldsDescriptions(name);
			descriptions.ForEach(productDefinitionField.AddProductDefinitionFieldDescription);

			return productDefinitionField;
		}

		private IEnumerable<ProductDefinitionFieldDescription> CreateDefinitionFieldsDescriptions(string displayName)
		{
			return new Collection<ProductDefinitionFieldDescription>
			{
				new ProductDefinitionFieldDescription
				{
					DisplayName = string.Format("{0}-US", displayName), 
					CultureCode = "en-US"
				},new ProductDefinitionFieldDescription
				{
					DisplayName = string.Format("{0}-GB", displayName), 
					CultureCode = "en-GB"
				}
			};
		}
	}
}
