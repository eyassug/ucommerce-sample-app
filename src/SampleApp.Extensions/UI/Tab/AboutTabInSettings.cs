using System;
using SampleApp.Extensions.Model;
using UCommerce.Pipelines;
using UCommerce.Pipelines.ViewBuilder;

namespace SampleApp.Extensions.UI.Tab
{
	public class AboutTabInSettings : IPipelineTask<IPipelineArgs<ViewBuilderRequest, ViewBuilderResponse>>
	{
		private readonly TabConfiguration _configuration;

		public AboutTabInSettings(TabConfiguration tabConfiguration)
		{
			_configuration = tabConfiguration;
		}

		public PipelineExecutionResult Execute(IPipelineArgs<ViewBuilderRequest, ViewBuilderResponse> subject)
		{
			if (subject.Request.ViewId != "settingsstartpage_aspx" || !_configuration.ShowTab) return PipelineExecutionResult.Success;

			var viewSection = new ViewSection
			{
				View = "../Apps/SampleApp/About.ascx",
				ResourceKey = "",
				MultiLingual = false,
				HasSaveButton = false,
				HasDeleteButton = false
			};

			subject.Response.ViewSections.Add(viewSection);

			return PipelineExecutionResult.Success;
		}
	}
}
