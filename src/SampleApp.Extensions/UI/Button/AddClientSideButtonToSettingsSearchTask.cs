using System.Linq;
using System.Web.UI.WebControls;
using UCommerce.Infrastructure.Globalization;
using UCommerce.Pipelines;
using UCommerce.Presentation;
using UCommerce.Presentation.UI;

namespace SampleApp.Extensions.UI.Button
{
	/// <summary>
	/// Adds a Client side button to the Settings search view. 
	/// </summary>
	public class AddClientSideButtonToSettingsSearchTask : IPipelineTask<SectionGroup>
	{
		private readonly IResourceManager _resourceManager;

		public AddClientSideButtonToSettingsSearchTask(IResourceManager resourceManager)
		{
			_resourceManager = resourceManager;
		}

		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			//Finds the right section by filtering on Name and OriginalName 
			var section = subject.Sections.FirstOrDefault(s => s.Name == "Common" && s.OriginalName == "IndexFromScratch.ascx");

			if (section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateClientSideButton());

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateClientSideButton()
		{
			var clientSideButton = new ImageButton();
			clientSideButton.ImageUrl = "/umbraco/ucommerce/Apps/SampleApp/Media/uCommerce-logo-symbol-small.png";
			clientSideButton.CausesValidation = false;

			var translatedConfirmText = _resourceManager.GetLocalizedText("SampleApp", "confirmClientSideButton");
			clientSideButton.Attributes.Add("onclick", "if (confirm('" + translatedConfirmText + "')) { window.location.replace('/'); } return false;");
			
			return clientSideButton;
		}
	}
}
