using SampleApp.Extensions.Model;
using UCommerce.EntitiesV2.UI.Impl;
using UCommerce.Pipelines;
using UCommerce.Pipelines.ViewBuilder;

namespace SampleApp.Extensions.UI.Tab
{
	/// <summary>
	/// A pipeline task that adds a new tab to the setting node in uCommerce
	/// </summary>
	public class AboutTabInSettings : IPipelineTask<IPipelineArgs<ViewBuilderRequest, ViewBuilderResponse>>
	{
		private readonly TabConfiguration _configuration;

		public AboutTabInSettings(TabConfiguration tabConfiguration)
		{
			_configuration = tabConfiguration;
		}

		public PipelineExecutionResult Execute(IPipelineArgs<ViewBuilderRequest, ViewBuilderResponse> subject)
		{
			// Checks if the view is the one that we want to hook into
			if (subject.Request.ViewId != "settingsstartpage_aspx" || !_configuration.ShowTab) return PipelineExecutionResult.Success;

			var viewSection = BuildViewSection();

			subject.Response.ViewSections.Add(viewSection);

			return PipelineExecutionResult.Success;
		}

		private ViewSection BuildViewSection()
		{
			var viewSection = new ViewSection
			{
				View = "../Apps/SampleApp/About.ascx",
				DisplayName = "About, DisplayName property",
				MultiLingual = false,
				HasSaveButton = false,
				HasDeleteButton = false
			};

			return viewSection;
		}
	}
}
