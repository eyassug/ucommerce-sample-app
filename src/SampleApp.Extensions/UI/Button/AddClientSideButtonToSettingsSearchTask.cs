using System.Linq;
using System.Web.UI.WebControls;
using UCommerce;
using UCommerce.Infrastructure.Apps;
using UCommerce.Infrastructure.Globalization;
using UCommerce.Pipelines;
using UCommerce.Presentation.UI;

namespace SampleApp.Extensions.UI.Button
{
	/// <summary>
	/// Adds a Client side button to the Settings search view. 
	/// </summary>
	public class AddClientSideButtonToSettingsSearchTask : IPipelineTask<SectionGroup>
	{
		private readonly IAppPathService _appPathService;

		public AddClientSideButtonToSettingsSearchTask(IAppPathService appPathService)
		{
			_appPathService = appPathService;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			if (subject.GetViewName() != Constants.UI.Pages.Settings.Search) return PipelineExecutionResult.Success;

			//Finds the right section by filtering on Name and OriginalName 
			var section = subject.Sections.FirstOrDefault(s => s.Name == "Common" && s.OriginalName == "IndexFromScratch.ascx");

			if (section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateClientSideButton());

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateClientSideButton()
		{
			var clientSideButton = new ImageButton();

			clientSideButton.ImageUrl = _appPathService.GetAppPath("/Apps/SampleApp/Media/uCommerce-logo-symbol-small.png");
			clientSideButton.CausesValidation = false;

			clientSideButton.Attributes.Add("onclick", "if (confirm('Are you sure you want to show your site?')) { window.location.replace('/'); } return false;");
			
			return clientSideButton;
		}
	}
}
