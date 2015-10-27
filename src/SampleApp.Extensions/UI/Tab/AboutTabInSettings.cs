using System;
using System.Web.UI;
using SampleApp.Extensions.Model;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
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
			var name = sectionGroup.GetName();
			//Check the view is the one what we want to add our tab to
			if (!_configuration.ShowTab || sectionGroup.View.ToString() != "ASP.umbraco_ucommerce_settings_settingsstartpage_aspx") return PipelineExecutionResult.Success;

			var section = BuildSection(sectionGroup);
			sectionGroup.AddSection(section);

			//Makes the new tab the default tab when the settings node is clicked.
			sectionGroup.ActiveTabId = sectionGroup.Sections.IndexOf(section);

			return PipelineExecutionResult.Success;
		}

		private Section BuildSection(SectionGroup sectionGroup)
		{
			var section = new Section
			{
				Name = "About",
				ID = CreateUniqueControlID(sectionGroup.View as Page)
			};

			var control = sectionGroup.View.LoadControl("../Apps/SampleApp/About.ascx");

			//Get the name of the control if it implements the INamed inferface
			if (control is INamed)
				section.Name = (control as INamed).Name;

			section.AddControl(control);
			return section;
		}

		private string CreateUniqueControlID(Page page)
		{
			Guard.Against.NullArgument(page);
			return page.ClientID + "_" + Guid.NewGuid();
		}
	}
}