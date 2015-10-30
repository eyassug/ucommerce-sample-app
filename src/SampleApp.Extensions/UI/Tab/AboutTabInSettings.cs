using SampleApp.Extensions.Configuration;
using UCommerce;
using UCommerce.EntitiesV2;
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
			//If the view is not the one that we want to hook into, then do nothing
			if (!_configuration.ShowTab || sectionGroup.GetViewName() != Constants.UI.Pages.Roots.Settings) return PipelineExecutionResult.Success;

			var section = BuildSection(sectionGroup);
			sectionGroup.AddSection(section);

			//Makes the new tab the default tab when the settings node is clicked
			sectionGroup.ActiveTabId = sectionGroup.Sections.IndexOf(section);

			return PipelineExecutionResult.Success;
		}

		private Section BuildSection(SectionGroup sectionGroup)
		{
			var section = new Section
			{
				Name = "About",
				ID = sectionGroup.CreateUniqueControlId()
			};

			var control = sectionGroup.View.LoadControl("/Apps/SampleApp/About.ascx");

			//Get the name of the control if it implements the INamed inferface
			if (control is INamed)
				section.Name = (control as INamed).Name;

			section.AddControl(control);
			return section;
		}
	}
}