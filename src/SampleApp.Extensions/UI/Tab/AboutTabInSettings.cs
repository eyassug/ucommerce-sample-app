using SampleApp.Extensions.Model;
using UCommerce.Pipelines;
using UCommerce.Presentation.UI;

namespace SampleApp.Extensions.UI.Tab
{
	/// <summary>
	/// A pipeline task that adds a new tab to the setting node in uCommerce
	/// </summary>
	public class AboutTabInSettings : IPipelineTask<SectionGroup>
	{
		private readonly TabConfiguration _configuration;

		public AboutTabInSettings(TabConfiguration tabConfiguration)
		{
			_configuration = tabConfiguration;
		}


		public PipelineExecutionResult Execute(SectionGroup sectionGroup)
		{
			//Check that we want to hook in
			if(!_configuration.ShowTab) return PipelineExecutionResult.Success;

			//Build section
			var section = BuildSection();

			//Add section to the section group
			sectionGroup.AddSection(section);

			return PipelineExecutionResult.Success;
		}


		private Section BuildSection()
		{
			//var section = new Section
			//{
			//	Name
			//};

			return null;
		}
	}
}