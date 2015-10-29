using System.Linq;
using System.Web.UI.WebControls;
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
		public PipelineExecutionResult Execute(SectionGroup subject)
		{
			//Finds the right section by filtering on Name and OriginalName 
			var section = subject.Sections.FirstOrDefault(s => s.Name == "Common" && s.OriginalName == "IndexFromScratch.ascx");

			if(section == null) return PipelineExecutionResult.Success;

			section.Menu.AddMenuButton(CreateClientSideButton());

			return PipelineExecutionResult.Success;
		}

		public ImageButton CreateClientSideButton()
		{
			var clientSideButton = new ImageButton();
			clientSideButton.ImageUrl = Resources.Images.Menu.Delete;
			clientSideButton.CausesValidation = false;
			clientSideButton.Attributes.Add("onclick", "if (confirm('Are you sure you want to this?')) { return true; } else return false;");
			return clientSideButton;
		}
	}
}
